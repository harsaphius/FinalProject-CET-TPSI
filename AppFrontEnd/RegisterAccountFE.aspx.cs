using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject.AppFrontEnd
{
    public partial class RegisterAccountFE : System.Web.UI.Page
    {
        string clientid = ConfigurationManager.AppSettings["clientid"];
        string clientsecret = ConfigurationManager.AppSettings["clientsecret"];
        string redirectionURL = ConfigurationManager.AppSettings["redirection_url"];
        string url = ConfigurationManager.AppSettings["url"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["code"] != null)
                {
                    GetToken(Request.QueryString["code"].ToString());
                }
            }
        }

        protected void btn_registar_Click(object sender, EventArgs e)
        {

        }

        public void GetToken(string code)
        {
            string poststring = "grant_type=authorization_code&code=" + code + "&client_id=" + clientid + "&client_secret=" + clientsecret + "&redirect_uri=" + redirectionURL + "";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            UTF8Encoding utfenc = new UTF8Encoding();
            byte[] bytes = utfenc.GetBytes(poststring);
            Stream outputstream = null;
            try
            {
                request.ContentLength = bytes.Length;
                outputstream = request.GetRequestStream();
                outputstream.Write(bytes, 0, bytes.Length);
            }
            catch { 
            }
            var response = (HttpWebResponse)request.GetResponse();
            var streamReader = new StreamReader(response.GetResponseStream());
            string responseFromServer = streamReader.ReadToEnd();

            //var responseData = new JavaScriptSerializer().Deserialize<dynamic>(responseFromServer);

            //string userEmail = responseData["email"];

            //tb_email.Text = userEmail;
            JavaScriptSerializer js = new JavaScriptSerializer();
            Classes.Tokenclass obj = js.Deserialize<Classes.Tokenclass>(responseFromServer);
            GetuserProfile(obj.access_token);
        }

        public void GetuserProfile(string accesstoken)
        {
            string url = "https://www.googleapis.com/oauth2/v2/userinfo?alt=json&access_token=" + accesstoken + "";
            WebRequest request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            response.Close();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Classes.Userclass userinfo = js.Deserialize<Classes.Userclass>(responseFromServer);
            tb_username.Text = userinfo.given_name;
            tb_email.Text = userinfo.email;
            //imgprofile.ImageUrl = userinfo.picture;
            //lblid.Text = userinfo.id;
            //lblgender.Text = userinfo.gender;
            //lbllocale.Text = userinfo.locale;
            //lblname.Text = userinfo.name;
            //hylprofile.NavigateUrl = userinfo.link;
        }
    }
}
