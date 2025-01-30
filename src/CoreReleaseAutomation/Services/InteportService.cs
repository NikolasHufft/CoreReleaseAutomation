using System;
using System.IO;
using System.Net;
using System.Xml;

namespace CoreReleaseAutomation.Services
{
    public static class InteportService
    {
        public static XmlDocument ExecuteRequestAsync(string url, XmlDocument xml)
        {            
            XmlDocument XMLResponse = null;
            HttpWebRequest objHttpWebRequest;
            HttpWebResponse objHttpWebResponse = null;           
            Stream objRequestStream = null;
            Stream objResponseStream = null;
            XmlTextReader objXMLReader;
            
            objHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            try
            {            
                byte[] bytes;
                bytes = System.Text.Encoding.ASCII.GetBytes(xml.InnerXml);
                objHttpWebRequest.Method = "POST";
                objHttpWebRequest.ContentLength = bytes.Length;
                objHttpWebRequest.ContentType = "text/xml; encoding='utf-8'";
             
                objRequestStream = objHttpWebRequest.GetRequestStream();

                objRequestStream.Write(bytes, 0, bytes.Length);

                objRequestStream.Close();

                objHttpWebResponse = (HttpWebResponse)objHttpWebRequest.GetResponse();

                if (objHttpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    objResponseStream = objHttpWebResponse.GetResponseStream();

                    objXMLReader = new XmlTextReader(objResponseStream);

                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(objXMLReader);

                    XMLResponse = xmldoc;

                    objXMLReader.Close();
                }

                objHttpWebResponse.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {                
                objRequestStream.Close();
                objResponseStream.Close();
                objHttpWebResponse.Close();
            }

            return XMLResponse;
        }
    }
}
