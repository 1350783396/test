function NewTab(tabid, name, url) {
    top.topManager.openPage({
        id: tabid,
        href: url,
        title: name
    });
}

function CloseTabItem(id) {

    if (parent.tabs || parent.tabPanel) {

        var centerFrame = parent.tabs != null ? parent.tabs : parent.tabPanel;

        centerFrame.remove(id);
    }

}