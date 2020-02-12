namespace XelionZR
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading;

    public class zResponder
    {
        private readonly HttpListener _listener;
        private readonly Func<HttpListenerRequest, string> _responderMethod;

        public zResponder(Func<HttpListenerRequest, string> method, params string[] prefixes) : this(prefixes, method)
        {
        }

        public zResponder(string[] prefixes, Func<HttpListenerRequest, string> method)
        {
            this._listener = new HttpListener();
            if (!HttpListener.IsSupported)
            {
                throw new NotSupportedException("Error Sistema Operativo no Soportado");
            }
            if (((prefixes == null) ? 1 : ((prefixes.Length == 0) ? 1 : 0)) != 0)
            {
                throw new ArgumentException("Prrefixes");
            }
            foreach (string str in prefixes)
            {
                this._listener.Prefixes.Add(str);
            }
            Func<HttpListenerRequest, string> func = method;
            if (func == null)
            {
                Func<HttpListenerRequest, string> local1 = func;
                throw new ArgumentException("method");
            }
            this._responderMethod = func;
            this._listener.Start();
        }

        public void Run(string url)
        {
            ThreadPool.QueueUserWorkItem(delegate (object o) {
                try
                {
                    while (true)
                    {
                        if (!this._listener.IsListening)
                        {
                            break;
                        }
                        ThreadPool.QueueUserWorkItem(delegate (object c) {
                            HttpListenerContext context = c as HttpListenerContext;
                            try
                            {
                                byte[] bytes = Encoding.UTF8.GetBytes(this._responderMethod(context.Request));
                                context.Response.ContentLength64 = bytes.Length;
                                context.Response.OutputStream.Write(bytes, 0, bytes.Length);
                                using (Stream stream = context.Request.InputStream)
                                {
                                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                                    {
                                        string str = reader.ReadToEnd();
                                    }
                                }
                            }
                            catch (Exception exception)
                            {
                                using (StreamWriter writer = new StreamWriter("./ExceptionError.txt", true))
                                {
                                    writer.Write(string.Format("Message: {0}<br />{1}StackTrace :{2}{1}Fecha :{3}{1}-----------------------------------------------------------------------------{1}", new object[] { exception.Message, Environment.NewLine, exception.StackTrace, DateTime.Now.ToString() }));
                                }
                            }
                            finally
                            {
                                context.Response.OutputStream.Close();
                            }
                        }, this._listener.GetContext());
                    }
                }
                catch (Exception exception)
                {
                    using (StreamWriter writer = new StreamWriter("./ExceptionError.txt", true))
                    {
                        writer.Write(string.Format("Message: {0}<br />{1}StackTrace :{2}{1}Fecha :{3}{1}-----------------------------------------------------------------------------{1}", new object[] { exception.Message, Environment.NewLine, exception.StackTrace, DateTime.Now.ToString() }));
                    }
                }
            });
        }
    }
}