using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;

namespace aiCorporation.NewImproved
{
    public class SalesAgentFileRecord
    {
        private string m_szSalesAgentName;
        private string m_szSalesAgentEmailAddress;
        private string m_szClientName;
        private string m_szClientIdentifier;
        private string m_szBankName;
        private string m_szAccountNumber;
        private string m_szSortCode;
        private string m_szCurrency;

        public string SalesAgentName { get { return (m_szSalesAgentName); } }
        public string SalesAgentEmailAddress { get { return (m_szSalesAgentEmailAddress); } }
        public string ClientName { get { return (m_szClientName); } }
        public string ClientIdentifier { get { return (m_szClientIdentifier); } }
        public string BankName { get { return (m_szBankName); } }
        public string AccountNumber { get { return (m_szAccountNumber); } }
        public string SortCode { get { return (m_szSortCode); } }
        public string Currency { get { return (m_szCurrency); } }

        public static string CsvHeader()
        {
            StringBuilder sbString;

            sbString = new StringBuilder();

            sbString.Append("SalesAgentName,SalesAgentEmailAddress,ClientName,ClientIdentifier,BankName,AccountNumber,SortCode,Currency\r\n");

            return (sbString.ToString());
        }

        public string ToCsvRecord()
        {
            StringBuilder sbString;

            sbString = new StringBuilder();

            sbString.AppendFormat("\"{0}\"", m_szSalesAgentName);
            sbString.AppendFormat(",\"{0}\"", m_szSalesAgentEmailAddress);
            sbString.AppendFormat(",\"{0}\"", m_szClientName);
            sbString.AppendFormat(",\"{0}\"", m_szClientIdentifier);
            sbString.AppendFormat(",\"{0}\"", m_szBankName);
            sbString.AppendFormat(",\"{0}\"", m_szAccountNumber);
            sbString.AppendFormat(",\"{0}\"", m_szSortCode);
            sbString.AppendFormat(",\"{0}\"", m_szCurrency);

            sbString.Append("\r\n");

            return (sbString.ToString());
        }

        public SalesAgentFileRecord(string szSalesAgentName,
                                    string szSalesAgentEmailAddress,
                                    string szClientName,
                                    string szClientIdentifier,
                                    string szBankName,
                                    string szAccountNumber,
                                    string szSortCode,
                                    string szCurrency)
        {
            m_szSalesAgentName = szSalesAgentName;
            m_szSalesAgentEmailAddress = szSalesAgentEmailAddress;
            m_szClientName = szClientName;
            m_szClientIdentifier = szClientIdentifier;
            m_szBankName = szBankName;
            m_szAccountNumber = szAccountNumber;
            m_szSortCode = szSortCode;
            m_szCurrency = szCurrency;
        }
    }
    public class SalesAgentFileRecordList
    {
        private List<SalesAgentFileRecord> m_lsafrSalesAgentFileRecordList;

        public int Count { get { return (m_lsafrSalesAgentFileRecordList.Count); } }

        public SalesAgentFileRecord this[int nIndex]
        {
            get
            {
                if (nIndex < 0 ||
                    nIndex >= m_lsafrSalesAgentFileRecordList.Count)
                {
                    throw new Exception("Array index out of bounds");
                }
                return (m_lsafrSalesAgentFileRecordList[nIndex]);
            }
        }

        public SalesAgentFileRecord this[string szSalesAgentEmailAddress]
        {
            get
            {
                int nCount = 0;
                bool boFound = false;
                SalesAgentFileRecord safrSalesAgentFileRecord = null;

                while (!boFound &&
                       nCount < m_lsafrSalesAgentFileRecordList.Count)
                {
                    if (m_lsafrSalesAgentFileRecordList[nCount].SalesAgentEmailAddress == szSalesAgentEmailAddress)
                    {
                        boFound = true;
                        safrSalesAgentFileRecord = m_lsafrSalesAgentFileRecordList[nCount];
                    }
                    nCount++;
                }
                return (safrSalesAgentFileRecord);
            }
        }

        public string ToCsvString()
        {
            StringBuilder sbCsvString;
            int nCount;

            sbCsvString = new StringBuilder();

            sbCsvString.Append(SalesAgentFileRecord.CsvHeader());

            for (nCount = 0; nCount < m_lsafrSalesAgentFileRecordList.Count; nCount++)
            {
                sbCsvString.AppendFormat("{0}", m_lsafrSalesAgentFileRecordList[nCount].ToCsvRecord());
            }

            return (sbCsvString.ToString());
        }

        public static SalesAgentFileRecordList FromCsvStream(Stream sStream)
        {
            StreamReader srReader;
            CsvReader crReader;
            SalesAgentFileRecord safrSalesAgentFileRecord;
            List<SalesAgentFileRecord> lsafrSalesAgentFileRecordList;
            SalesAgentFileRecordList safrlSalesAgentFileRecordList;
            string szSalesAgentName;
            string szSalesAgentEmailAddress;
            string szClientName;
            string szClientIdentifier;
            string szBankName;
            string szAccountNumber;
            string szSortCode;
            string szCurrency;
            int nCount;

            lsafrSalesAgentFileRecordList = new List<SalesAgentFileRecord>();

            if (sStream != null)
            {
                nCount = 0;

                srReader = new StreamReader(sStream);
                crReader = new CsvReader(srReader);

                while (crReader.Read())
                {
                    // don't read in the first row as it's the header data
                    if (nCount > 0)
                    {
                        szSalesAgentName = crReader.GetField<string>(0);
                        szSalesAgentEmailAddress = crReader.GetField<string>(1);
                        szClientName = crReader.GetField<string>(2);
                        szClientIdentifier = crReader.GetField<string>(3);
                        szBankName = crReader.GetField<string>(4);
                        szAccountNumber = crReader.GetField<string>(5);
                        szSortCode = crReader.GetField<string>(6);
                        szCurrency = crReader.GetField<string>(7);

                        safrSalesAgentFileRecord = new SalesAgentFileRecord(szSalesAgentName, szSalesAgentEmailAddress, szClientName, szClientIdentifier, szBankName, szAccountNumber, szSortCode, szCurrency);
                        lsafrSalesAgentFileRecordList.Add(safrSalesAgentFileRecord);
                    }
                    nCount++;
                }
            }
            safrlSalesAgentFileRecordList = new SalesAgentFileRecordList(lsafrSalesAgentFileRecordList);

            return (safrlSalesAgentFileRecordList);
        }

        /************************************************************/
        /* THIS IS THE FUNCTION THAT WE WOULD LIKE YOU TO IMPLEMENT */
        /************************************************************/
        public SalesAgentList ToSalesAgentList()
        {
            int nCount = 0;
            List<SalesAgent> lsaSaleAgentList = new List<SalesAgent>();
            List<BankAccountBuilder> lbaBankAccountBuilderList = new List<BankAccountBuilder>();
            SalesAgentBuilder saSalesAgentBuilder = null;
            ClientBuilder cClientBuilder = null;
            BankAccountBuilder baBankAccountBuilder = null;
            string szSalesAgentEmailAddressCompare = null;
            string szClientIdentifierCompare = null;

            while (nCount < m_lsafrSalesAgentFileRecordList.Count)
            {
                var currentRecord = m_lsafrSalesAgentFileRecordList[nCount];

                // Initial SalesAgent at first record
                if (nCount == 0 || szSalesAgentEmailAddressCompare != currentRecord.SalesAgentEmailAddress)
                {
                    // If not the first record or the SalesAgent has changed, add the previous SalesAgent
                    if (nCount > 0)
                    {
                        // Add the previous SalesAgent and add previous Client if SalesAgentEmailAddress has changed
                        if (saSalesAgentBuilder != null)
                        {
                            if (cClientBuilder != null)
                            {
                                for (int baCount = 0; baCount < lbaBankAccountBuilderList.Count; baCount++)
                                {
                                    cClientBuilder.BankAccountList.Add(lbaBankAccountBuilderList[baCount]);
                                }
                                saSalesAgentBuilder.ClientList.Add(cClientBuilder);

                                // Clear Client after assigned to SaleAgent
                                cClientBuilder = null;
                            }

                            lsaSaleAgentList.Add(saSalesAgentBuilder.ToSalesAgent());
                        }
                    }

                    // Recreate new current SalesAgent
                    saSalesAgentBuilder = new SalesAgentBuilder
                    {
                        SalesAgentName = currentRecord.SalesAgentName,
                        SalesAgentEmailAddress = currentRecord.SalesAgentEmailAddress
                    };

                    // Clear previous BankAccount lists
                    lbaBankAccountBuilderList.Clear();
                }

                // Compare with the previous Client 
                if (szClientIdentifierCompare != currentRecord.ClientIdentifier)
                {
                    // If the Client has changed, add the previous Client and Bank Accounts to the current SalesAgent
                    if (cClientBuilder != null)
                    {
                        for (int baCount = 0; baCount < lbaBankAccountBuilderList.Count; baCount++)
                        {
                            cClientBuilder.BankAccountList.Add(lbaBankAccountBuilderList[baCount]);
                        }

                        saSalesAgentBuilder.ClientList.Add(cClientBuilder);
                    }

                    // Create a new Client
                    cClientBuilder = new ClientBuilder
                    {
                        ClientName = currentRecord.ClientName,
                        ClientIdentifier = currentRecord.ClientIdentifier
                    };

                    // Clear BankAccount list
                    lbaBankAccountBuilderList.Clear();
                }

                // Add the current BankAccount to the list
                baBankAccountBuilder = new BankAccountBuilder
                {
                    BankName = currentRecord.BankName,
                    AccountNumber = currentRecord.AccountNumber,
                    SortCode = currentRecord.SortCode,
                    Currency = currentRecord.Currency
                };

                lbaBankAccountBuilderList.Add(baBankAccountBuilder);

                // Update the comparison values for the next record
                szSalesAgentEmailAddressCompare = currentRecord.SalesAgentEmailAddress;
                szClientIdentifierCompare = currentRecord.ClientIdentifier;

                nCount++;
            }

            // Add last SalesAgent and Clients after loop
            if (saSalesAgentBuilder != null)
            {
                if (cClientBuilder != null)
                {
                    for (int baCount = 0; baCount < lbaBankAccountBuilderList.Count; baCount++)
                    {
                        cClientBuilder.BankAccountList.Add(lbaBankAccountBuilderList[baCount]);
                    }
                    saSalesAgentBuilder.ClientList.Add(cClientBuilder);
                }
                lsaSaleAgentList.Add(saSalesAgentBuilder.ToSalesAgent());
            }

            return new SalesAgentList(lsaSaleAgentList);

        }


        public SalesAgentFileRecordList(List<SalesAgentFileRecord> lsafrSalesAgentFileRecordList)
        {
            int nCount = 0;

            m_lsafrSalesAgentFileRecordList = new List<SalesAgentFileRecord>();

            if (lsafrSalesAgentFileRecordList != null)
            {
                for (nCount = 0; nCount < lsafrSalesAgentFileRecordList.Count; nCount++)
                {
                    m_lsafrSalesAgentFileRecordList.Add(lsafrSalesAgentFileRecordList[nCount]);
                }
            }
        }

        public List<SalesAgentFileRecord> GetListOfSalesAgentFileRecordObjects()
        {
            List<SalesAgentFileRecord> lsafrSalesAgentFileRecordList = null;
            int nCount = 0;

            lsafrSalesAgentFileRecordList = new List<SalesAgentFileRecord>();

            for (nCount = 0; nCount < m_lsafrSalesAgentFileRecordList.Count; nCount++)
            {
                lsafrSalesAgentFileRecordList.Add(m_lsafrSalesAgentFileRecordList[nCount]);
            }

            return (lsafrSalesAgentFileRecordList);
        }
    }
}