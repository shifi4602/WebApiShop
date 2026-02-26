using Zxcvbn;
using Enteties;

namespace Services
{
    public class passwordServices : IpasswordServices
    {
        public PassEntity GetStrength(string password)
        {
            if (password != null && password != "")
            {
                var result = Zxcvbn.Core.EvaluatePassword(password);
                if (result != null)
                {
                    int strengthPassword = result.Score;
                    PassEntity passEntity = new PassEntity();
                    passEntity.Password = password;
                    passEntity.Strength = strengthPassword;
                    return passEntity;
                }
            }
            return null;
        }
    }
}
