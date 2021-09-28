using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using CyberSource.Model;
using CyberSource.Api;
using Cybersource_rest_samples_dotnet;
using System.ServiceModel.Web;
using System.Text;

namespace technical_assessmen
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IService1
    {
       
        //autorizacion
        public CyberSource.Model.PtsV2PaymentsPost201Response CreatePayment(CreatePaymentRequest request)
        {
         
            try
            {
                if (request is null)
                {
                    return null;
                }

                var configDictionary = new Configuration().GetConfiguration();
                var clientConfig = new CyberSource.Client.Configuration(merchConfigDictObj: configDictionary);
                var apiInstance = new PaymentsApi(clientConfig);
                CyberSource.Model.PtsV2PaymentsPost201Response result = apiInstance.CreatePayment(request);

                //guardar en bd
                bool flag = true;
              
                CybersourceDataContextDataContext CyberDataContext = new CybersourceDataContextDataContext();
                Transaction obj = new Transaction();
                // fields to be insert
                obj.IdTransaction = result.Id;
                obj.Status = result.Status;
                obj.Amount = result.OrderInformation.AmountDetails.AuthorizedAmount;
                obj.Capture = flag;
                CyberDataContext.Transactions.InsertOnSubmit(obj);
                // executes the commands to implement the changes to the database
                CyberDataContext.SubmitChanges();

                if (request.ProcessingInformation == null)
                {
                  CapturePayment(result.ClientReferenceInformation.Code, result.OrderInformation.AmountDetails.AuthorizedAmount, result.OrderInformation.AmountDetails.Currency, result.Id);
                }

                return result;

            }
            catch (Exception e)
            {
                return null;
            }


        }
        //captura
         public static string CapturePayment(string clientCode, string Amount, string Currency, string Id) 
        {

            try
            {
                Ptsv2paymentsClientReferenceInformation clientReferenceInformation = new Ptsv2paymentsClientReferenceInformation(Code: clientCode);
                Ptsv2paymentsidcapturesOrderInformationAmountDetails orderInformationAmountDetails = new Ptsv2paymentsidcapturesOrderInformationAmountDetails(TotalAmount: Amount, Currency: Currency);
                Ptsv2paymentsidcapturesOrderInformation orderInformation = new Ptsv2paymentsidcapturesOrderInformation(AmountDetails: orderInformationAmountDetails);

                var requestObj = new CapturePaymentRequest(
                ClientReferenceInformation: clientReferenceInformation,
                OrderInformation: orderInformation);

                var configDictionary = new Configuration().GetConfiguration();
                var clientConfig = new CyberSource.Client.Configuration(merchConfigDictObj: configDictionary);

              
               
                var apiInstance = new CaptureApi(clientConfig);
                PtsV2PaymentsCapturesPost201Response result = apiInstance.CapturePayment(requestObj, Id);

                //actualizar en bd
                CybersourceDataContextDataContext CyberDataContext = new CybersourceDataContextDataContext();

                var query = (from a in CyberDataContext.Transactions
                             where a.IdTransaction == Id
                             select a).FirstOrDefault();

                query.Status = result.Status;
                query.Capture = true;

                CyberDataContext.SubmitChanges();


                return result.Status;

            }
            catch (Exception e)
            {
                return null;
            }


        }

        //revesal
        public CyberSource.Model.PtsV2PaymentsReversalsPost201Response Reversal(AuthReversalRequest request, String id)
        {
         
            try
            {
                if (request is null)
                {
                    return null;
                }

                var configDictionary = new Configuration().GetConfiguration();
                 var clientConfig = new CyberSource.Client.Configuration(merchConfigDictObj: configDictionary);
                var apiInstance = new ReversalApi(clientConfig);
                PtsV2PaymentsReversalsPost201Response result = apiInstance.AuthReversal(id, request);

                //actualizar en bd
                CybersourceDataContextDataContext CyberDataContext = new CybersourceDataContextDataContext();

                var query = (from a in CyberDataContext.Transactions
                             where a.IdTransaction == id
                             select a).FirstOrDefault();

                query.Status = result.Status;

                CyberDataContext.SubmitChanges();

                return result;
            }
            catch (Exception e)
            {
                return null;
            }


        }

        //refund
        public CyberSource.Model.PtsV2PaymentsRefundPost201Response Refund(RefundPaymentRequest request,String id)
        {
         
            try
            {

                if (request is null)
                {
                    return null;
                }
                var configDictionary = new Configuration().GetConfiguration();
                var clientConfig = new CyberSource.Client.Configuration(merchConfigDictObj: configDictionary);

                var apiInstance = new RefundApi(clientConfig);
                PtsV2PaymentsRefundPost201Response result = apiInstance.RefundPayment(request, id);

                //actualizar en bd
                CybersourceDataContextDataContext CyberDataContext = new CybersourceDataContextDataContext();

                var query = (from a in CyberDataContext.Transactions
                             where a.IdTransaction == id
                             select a).FirstOrDefault();

                query.Status = result.Status;

                CyberDataContext.SubmitChanges();
                return result;
            }
            catch (Exception e)
            {
                return null;
            }


        }

        //DecisionManager
        public CyberSource.Model.RiskV1DecisionsPost201Response DecisionManager(CreateBundledDecisionManagerCaseRequest request)
        {
            
            try
            {
                var configDictionary = new Configuration().GetConfiguration();
                 var clientConfig = new CyberSource.Client.Configuration(merchConfigDictObj: configDictionary);

                if(request is null)
                {
                    return null;
                }
                var apiInstance = new DecisionManagerApi(clientConfig);
                RiskV1DecisionsPost201Response result = apiInstance.CreateBundledDecisionManagerCase(request);
              
                return result;
            }
            catch (Exception e)
            {
                var error = e.Message;
                return null;
            }


        }
    }
  
}
