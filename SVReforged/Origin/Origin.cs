using StardewModdingAPI.Events;
using StardewValley;
using SVReforged.EventHandler;
using SVReforged.HarmonyPatches;
using Object = StardewValley.Object;

namespace SVReforged.Origin;

public class Origin
{
    public string _jojaRole = "intern";

    public Origin()
    {
        ModEntry.SHelper.Events.GameLoop.DayStarted += OnDayStarted;
    }

    public void OnDayStarted(object sender, DayStartedEventArgs e)
    {
        if (Game1.year == 1 && Game1.currentSeason == "spring" && Game1.dayOfMonth == 1) ShowJojaRoleQuestion();
        ApplyRolePatches();
    }

    private void GiveDayOneItems(Farmer who, string dialogueId)
    {
        switch (_jojaRole)
        {
            case "accountant":
                Game1.player.Money += 250;
                break;

            case "energyconsultant":
                Game1.player.addItemToInventory(ItemRegistry.Create("(BC)231"));
                break;

            case "fisheriesmanager":
                Game1.player.addItemToInventory(ItemRegistry.Create("710"));
                break;

            case "hrmanager":
                Game1.player.addItemToInventory(ItemRegistry.Create("StardropTea"));
                break;

            case "miningsupervisor":
                Game1.player.addItemToInventory(ItemRegistry.Create("749", 10));
                break;

            case "salesexecutive":
                var item = ItemRegistry.Create("Book_PriceCatalogue");
                (item as Object).Price = 0;
                Game1.player.addItemToInventory(item);
                break;

            case "siteacquisitionlead":
                Game1.player.addItemToInventory(ItemRegistry.Create("709", 20));
                break;

            case "warehousemanager":
                Game1.player.addItemToInventory(ItemRegistry.Create("(BC)BigChest"));
                break;

            case "intern":
                break;

            default:
                throw new ArgumentException($"Unrecognized Joja role: {_jojaRole}");
        }
    }

    public void ApplyRolePatches()
    {
        switch (_jojaRole)
        {
            case "accountant":
                AccountantPatch.ApplyPatch();
                break;

            case "energyconsultant":
                EnergyConsultantEventHandler.InitiaizeEventListener();
                break;

            case "fisheriesmanager":
                FisheriesManagerPatch.ApplyPatch();
                break;

            case "hrmanager":
                HRManagerPatch.ApplyPatch();
                break;

            case "miningsupervisor":
                MiningSupervisorEventHandler.InitiaizeEventListener();
                break;

            case "salesexecutive":
                SalesExecutivePatch.ApplyPatch();
                break;

            case "siteacquisitionlead":
                SiteAcquisitionLeadEventHandler.InitiaizeEventListener();
                break;

            case "warehousemanager":
                WarehouseManagerPatch.ApplyPatch();
                break;

            case "intern":
                break;

            default:
                throw new ArgumentException($"Unrecognized Joja role: {_jojaRole}");
        }
    }

    private void ShowJojaRoleQuestion()
    {
        var choices = new List<Response>
        {
            new("accountant", "Accountant"),
            new("warehousemanager", "Warehouse Manager"),
            new("miningsupervisor", "Mining Supervisor"),
            new("hrmanager", "HR Manager"),
            new("energyconsultant", "Energy Consultant"),
            new("fisheriesmanager", "Fisheries Manager"),
            new("salesexecutive", "Sales Executive"),
            new("siteacquisitionlead", "Site Acquisition Lead")
        };

        Game1.currentLocation.createQuestionDialogue(
            "What was your role at Joja Corp?",
            choices.ToArray(),
            GiveDayOneItems
        );
    }
}