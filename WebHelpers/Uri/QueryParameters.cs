using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace WebHelpers.Uri
{
    public class QueryParameters
    {

        private Dictionary<string, string> parameters;

        public string this[string name] {
            get { return this.GetParam(name); }
            set { this.Add(name, value); }
        }

        public QueryParameters()
        {
            parameters = new Dictionary<string, string>();
        }
        public QueryParameters(Dictionary<string, string> queryParameters) : this()
        {
			if (queryParameters == null) {
				return;
			}
			parameters = queryParameters;
        }
        public QueryParameters(string queryString, char paramSeparator = '&') : this()
        {
			if (queryString == null) {
				return;
			}
			if (queryString[0] == '?') {
				queryString = queryString.Substring(1);
			}
			var queryParameters = queryString.Split(paramSeparator);
            foreach (var qp in queryParameters) {
                var qp2 = qp.Split('=');
                this.Add(
                    WebUtility.UrlDecode(qp2[0]),
                    WebUtility.UrlDecode(qp2[1]),
                    true
                );
            }
        }

        public void Add(string name, string value, bool overwrite = false)
        {
            if (overwrite && this.parameters.ContainsKey(name)) {
                this.parameters.Remove(name);
            }
            this.parameters.Add(name, value);
        }
        public void Append(Dictionary<string, string> parameters, bool overwrite = false)
        {
            IEnumerable<KeyValuePair<string, string>> concat;
            if (overwrite) {
                concat = parameters.Concat(this.parameters.Where(x => !parameters.ContainsKey(x.Key)));
            }
            else {
                concat = this.parameters.Concat(parameters);
            }
            this.parameters = concat.ToDictionary(x => x.Key, x => x.Value);
        }
        public bool ContainsParam(string name)
        {
            return parameters.ContainsKey(name);
        }
        public string GetParam(string name)
        {
            if (parameters.TryGetValue(name, out string value)) {
                return value;
            }
            return null;
        }
        public bool Remove(string name)
        {
            return this.parameters.Remove(name);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var param in parameters) {
                sb.Append(string.Format("{0}={1}&",
                    WebUtility.UrlEncode(param.Key),
                    WebUtility.UrlEncode(param.Value)
                ));
            }
            if (sb.Length > 1) {
                sb.Remove(sb.Length - 1, 1);
			}
            return sb.ToString();
        }
		public override bool Equals(object obj)
		{
            return (ToString() == obj.ToString());
		}
	}
}
