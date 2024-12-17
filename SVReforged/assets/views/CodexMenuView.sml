<lane z-index="1" orientation="vertical">
    <lane orientation="horizontal">
        <tab *repeat={Tabs} layout="64px" tooltip={Name} active={<>Active} activate=|^OnTabActivated(Name)|>
            <image layout="32px" sprite={Sprite} vertical-alignment="middle" />
        </tab>
    </lane>
    
    <frame 
        layout="880px 600px" 
        background={@Mods/StardewUI/Sprites/ControlBorder}>
        <label text="tab name" />
    </frame>
</lane>