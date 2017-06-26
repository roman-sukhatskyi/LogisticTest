using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace UM_LOGISTIC_V1.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/Client/Template").Include(
                "~/Client/Templates/TemplateModule.js",
                "~/Client/Templates/TemplateService.js",
                "~/Client/Templates/TemplateController.js"));

            bundles.Add(new ScriptBundle("~/bundles/Client/Modules").Include(
                "~/Client/Modules/MainModule.js",
                "~/Client/Constants/ModuleConstants.js",
                "~/Client/Directives/ConfirmClick.js"));

            bundles.Add(new ScriptBundle("~/bundles/Client/Services").Include(
                "~/Client/Services/Login/LoginService.js",
				"~/Client/Services/Account/AccountService.js",
				"~/Client/Services/Cooperation/CooperationService.js",
				"~/Client/Services/Session/SessionService.js",
				"~/Client/Services/Transportation/TransportationService.js",
                "~/Client/Services/User/UserService.js",
				"~/Client/Services/ApplicationPicture/ApplicationPictureService.js",
                "~/Client/Services/Notification/NotificationService.js",
                "~/Client/Services/Form/FormHelper.js",
                "~/Client/Services/Filter/FilterService.js",
                "~/Client/Services/ClientTask/ClientTaskService.js",
                "~/Client/Services/ApplicationTrash/ApplicationTrashService.js",
                "~/Client/Services/Event/EventService.js"));

            bundles.Add(new ScriptBundle("~/bundles/Client/Controllers").Include(
				"~/Client/Controllers/Profile/ProfileController.js",
				"~/Client/Controllers/Login/LoginController.js",
                "~/Client/Controllers/User/UserController.js",
				"~/Client/Controllers/Home/HomeController.js",
                "~/Client/Controllers/Cooperation/CooperationController.js",
                "~/Client/Controllers/Cooperation/CooperationApplicationController.js",
				"~/Client/Controllers/Transportation/TransportationController.js",
                "~/Client/Controllers/Transportation/TransportationApplicationController.js",
                "~/Client/Controllers/Transportation/TransportationApplicationDetailController.js",
                "~/Client/Controllers/Cooperation/CooperationApplicationDetailController.js",
                "~/Client/Controllers/RegistrationAccount/RegisterAccountController.js",
                "~/Client/Controllers/NotFilteredApplications/NotFilteredApplicationsController.js",
                "~/Client/Controllers/User/CreateUserController.js",
                "~/Client/Controllers/ClientTask/CallFeedbackController.js",
                "~/Client/Controllers/ClientTask/TasksController.js",
                "~/Client/Controllers/MyApplications/MyApplicationsController.js",
                "~/Client/Controllers/Transportation/TransportationEditController.js",
                "~/Client/Controllers/TransportMap/TransportMapController.js",
                "~/Client/Controllers/Cooperation/CooperationEditController.js"));

            bundles.Add(new ScriptBundle("~/bundles/Client/Libraries").Include(
                "~/Scripts/angular.min.js",
                "~/Scripts/angular-cookies.min.js",
                "~/Scripts/angular-ui-router.min.js",
                "~/Scripts/angular-route.min.js",
                "~/Scripts/angular-messages.min.js",
                "~/Scripts/jquery-1.10.2.min.js",
                //"~/Scripts/notify.min.js",
                "~/Scripts/bootbox.min.js",
                "~/Scripts/bootstrap-slider.js",
                "~/Scripts/ui-bootstrap-2.5.0.min.js",
                "~/Scripts/angular-bootstrap-lightbox.min.js",
                "~/Scripts/bootstrap-datepicker.min.js",
                "~/Scripts/jquery.imageview.js",
                "~/Scripts/lightbox.min.js",
                "~/Scripts/bootstrap-formhelpers-phone.js",
                "~/Scripts/jquery.maskedinput.min.js",
                "~/Scripts/bootstrap-formhelpers-number.js",
                "~/Scripts/jquery.signalR-2.2.2.min.js",
                "~/Scripts/bootstrap-notify.min.js",
                "~/Scripts/ekko-lightbox.min.js"/*,
                "~/Scripts/google-maps.js",
                "~/Scripts/angular-google-maps.min.js"*/));

            bundles.Add(new StyleBundle("~/bundles/Styles").Include(
                "~/Style/angular-bootstrap-lightbox.min.css",
                "~/Style/my-style.css",
                "~/Style/slider/bootstrap-slider.css",
                "~/Style/slider/rules.less",
                "~/Style/slider/variables.less",
                "~/Style/font-awesome-4.7.0/css/font-awesome.min.css",
                "~/Style/date-picker/bootstrap-datepicker3.css",
                "~/Style/lightbox/jquery.imageview.css",
                "~/Style/lightbox/lightbox.min.css",
                "~/Style/input-mask/bootstrap-form-helper.css",
                "~/Style/input-mask/ekko-lightbox.min.css",
                 "~/Style/bootswatch/bootstrap.css"));
        }
    }
}