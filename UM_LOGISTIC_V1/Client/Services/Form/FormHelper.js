mainModule.service('FormHelper', function (moduleConstants) {
    this.getFormValue = function (value) {
        if(!value || value == null || value == '' || value == 0)
        {
            value = moduleConstants.emptyFormValue;
        }
        return value;
    }

    this.getBooleanText = function (value) {
        var boolText = "";
        switch (value) {
            case false:
                boolText = "Ні";
                break;
            case true:
                boolText = "Так";
                break;
            default:
                boolText = moduleConstants.emptyTextValue;
                break;
        }
        return boolText;
    }
});