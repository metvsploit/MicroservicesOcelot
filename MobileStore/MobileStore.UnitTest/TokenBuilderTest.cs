using MobileStore.Authentication.Domain.Service;
using Xunit;

namespace MobileStore.UnitTest
{
    public class TokenBuilderTest
    {
        [Fact]
        public void Return_Token_NotNull()
        {
            //Arrange
            TokenBuilder tokenBuilder = new TokenBuilder();
            //Act
            var token = tokenBuilder.BuildToken("user@mail.ru");
            //Assert
            Assert.NotNull(token);
        }

        [Fact]
        public void Tokens_Are_Not_Equal_For_Different_Users()
        {
            TokenBuilder tokenBuilder = new TokenBuilder();
            //Arange
            var token1 = tokenBuilder.BuildToken("user1@mail.ru");
            //Act
            var token2 = tokenBuilder.BuildToken("user2@mail.ru");
            //Assert
            Assert.NotEqual(token1, token2);
        }
    }
}
