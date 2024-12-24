<lane orientation="vertical" horizontal-content-alignment="start">
    <lane orientation="horizontal" horizontal-content-alignment="start">
        <tab *repeat={Tabs} layout="64px" tooltip={tabName} active={<>IfTabActive} activate=|^OnTabActivatedSetOtherTabsInactive(tabIndex)|>
            <image layout="32px" sprite={Sprite} vertical-alignment="middle" horizontal-alignment="middle"/>
        </tab>
    </lane>
</lane>