using KCode.SMITEAPI;
using KCode.SMITEAPI.Reference;
using System;
using Xunit;

namespace KCode.SMITEAPITest
{
    public class MethodFillerTest
    {
        [Fact]
        public void MethodsAndSessionDataTest()
        {
            var filler = Methods.Createsession.Fill();
            filler._timestamp = "20200220202020";
            var actual = filler.Format(DataFormat.JSON).Auth(new SessionData(123, "PseudoAuthKey")).Timestamp().Finish();

            var expected = "/createsessionjson/123/fb2cd65834a8dd782314c939c71810b6/20200220202020";
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IncompleteFillThrows()
        {
            Assert.Throws<InvalidOperationException>(() => Methods.Createsession.Fill().Finish());
        }
    }
}
