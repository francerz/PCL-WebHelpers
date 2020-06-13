using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WebHelpers.Uri;

namespace WebHelpersTest.Uri
{
	[TestClass]
	public class QueryParametersTests
	{
		[TestMethod]
		public void TestToStringEmpty()
		{
			var qp = new QueryParameters();

			Assert.AreEqual(qp.ToString(), string.Empty);
		}
		[TestMethod]
		public void TestNewNullDictionary()
		{
			var expected = new QueryParameters();

			Dictionary<string, string> v = null;
			var actual = new QueryParameters(v);

			Assert.AreEqual(expected, actual);
		}
		[TestMethod]
		public void TestNewNullString()
		{
			var expected = new QueryParameters();

			string v = null;
			var actual = new QueryParameters(v);

			Assert.AreEqual(expected, actual);
		}
	}
}
