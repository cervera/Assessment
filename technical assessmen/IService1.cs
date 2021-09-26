using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using Newtonsoft.Json;
using CyberSource.Model;
using System.ServiceModel.Web;
using System.Text;

namespace technical_assessmen
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "CreatePayment", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        CyberSource.Model.PtsV2PaymentsPost201Response CreatePayment(CreatePaymentRequest request);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "Reversal/{id}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        CyberSource.Model.PtsV2PaymentsReversalsPost201Response Reversal(AuthReversalRequest request, String id);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "Refund/{id}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        CyberSource.Model.PtsV2PaymentsRefundPost201Response Refund(RefundPaymentRequest request,String id);

        [OperationContract]
        [WebInvoke(Method = "*", UriTemplate = "DecisionManager", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        CyberSource.Model.RiskV1DecisionsPost201Response DecisionManager(CreateBundledDecisionManagerCaseRequest request);
    }


    // Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
    //[DataContract]
    //public class PtsV2PaymentsPost201Response
    //{
    //    [DataMember]
    //    [JsonProperty]
    //    public string ResponseCode { get; set; }
    //    [DataMember]
    //    [JsonProperty]
    //    public string ResponseMessage { get; set; }
    //    [DataMember]
    //    [JsonProperty]
    //    public string RequestID { get; set; }
    //    [DataMember]
    //    [JsonProperty]
    //    public string Amount { get; set; }

    //}
    }