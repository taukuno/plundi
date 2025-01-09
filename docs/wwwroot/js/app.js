window.dialogFunctions = {
    openDialog: function (dialogId) {
        const dialog = document.getElementById(dialogId);
        if (!dialog)
            return;

        dialog.showModal();
    },
    closeDialog: function (dialogId) {
        const dialog = document.getElementById(dialogId);
        if (!dialog)
            return;

        dialog.close();
    }
};