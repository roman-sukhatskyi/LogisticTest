using System.Web.Routing;

namespace UM_LOGISTIC_V1.Routing
{
    public class DefaultRoute : Route
    {
        public DefaultRoute()
            : base("{*path}", new DefaultRouteHandler())
        {
            this.RouteExistingFiles = false;
        }
    }
}
