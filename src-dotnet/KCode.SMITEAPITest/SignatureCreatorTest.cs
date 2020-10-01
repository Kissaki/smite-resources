using Xunit;

namespace KCode.SMITEAPI
{
    public class SignatureCreatorTest
    {
        [Fact]
        public void TestUpstreamDocsCase()
        {
            var devId = 1004;
            var method = "createsession";
            var authKey = "23DF3C7E9BD14D84BF892AD206B6755C";
            var timestamp = "20120927183145";

            Assert.Equal("1004createsession23DF3C7E9BD14D84BF892AD206B6755C20120927183145", SignatureCreator.CreateString(devId, method, authKey, timestamp));

            Assert.Equal("8f53249be0922c94720834771ad43f0f", SignatureCreator.Hash("1004createsession23DF3C7E9BD14D84BF892AD206B6755C20120927183145"));
        }

        [Fact]
        public void TestHash1()
        {
            var str = "123createsessionAuthKey20200220202020";
            var expected = "9cc4be9a7be10b61f98827f53870ef9e";
            Assert.Equal(expected, SignatureCreator.Hash(str));
        }

        [Fact]
        public void TestHash2()
        {
            var str = "123createsessionPseudoAuthKey20200220202020";
            var expected = "fb2cd65834a8dd782314c939c71810b6";
            Assert.Equal(expected, SignatureCreator.Hash(str));
        }

        [Fact]
        public void TestCreate()
        {
            var expected = "9cc4be9a7be10b61f98827f53870ef9e";
            Assert.Equal(expected, SignatureCreator.Create(123, "createsession", "AuthKey", "20200220202020"));
        }

        [Fact]
        public void TestCreateString()
        {
            Assert.Equal("123createsessionAuthKey20200220202020", SignatureCreator.CreateString(123, "createsession", "AuthKey", "20200220202020"));
        }
    }
}
