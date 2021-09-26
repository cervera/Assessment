using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webClient
{
    public class Data
    {
        public  List<Transaction> InfoTransaction()
        {

            try
            {
                RefundReversalDataContext Transactions = new RefundReversalDataContext();

                var query = from a in Transactions.Transactions
                            select a;
                List<Transaction> s = query.ToList<Transaction>();
              
              
                return s;
            }
            catch (Exception e)
            {
                return null;
            }


        }
      

    }
}