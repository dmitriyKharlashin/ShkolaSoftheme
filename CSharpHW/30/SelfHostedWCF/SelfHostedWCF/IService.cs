using System.ServiceModel;
using System.ServiceModel.Web;

namespace SelfHostedWCF
{
    [ServiceContract()]
    public interface IService
    {
        [OperationContract()]
        [WebGet(UriTemplate = "/EchoWithGet?value={value}",
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json)]
        string EchoWithGet(string value);

        [OperationContract()]
        [WebInvoke(Method = "POST", UriTemplate = "/EchoWithPost",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        string EchoWithPost(string value);

    }
}