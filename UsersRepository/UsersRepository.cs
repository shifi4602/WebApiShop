using System.Text.Json;
using Enteties;

namespace UsersRepositories
{
    public class UsersRepository
    {
        string filePath = "M:\\WebApi\\WebApiShop\\WebApiShop\\users.txt";
        public Users AddUser(Users value)
        {
            int numberOfUsers = System.IO.File.ReadLines(filePath).Count();
            value.id = numberOfUsers + 1;
            string userJson = JsonSerializer.Serialize(value);
            System.IO.File.AppendAllText(filePath, userJson + Environment.NewLine);
            return value;
        }

        public Users login(UpdateUser value)
        {
            using (StreamReader reader = System.IO.File.OpenText(filePath))
            {
                string? currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {
                    Users userFromFile = JsonSerializer.Deserialize<Users>(currentUserInFile);
                    if (userFromFile.Email == value.Email && userFromFile.Password == value.Password)
                        return userFromFile;
                }
            }
            return null;
        }

        public void UpdateUser(int id, Users userToUpdate)
        {
            string textToReplace = string.Empty;
            using (StreamReader reader = System.IO.File.OpenText(filePath))
            {
                string currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {

                    Users user = JsonSerializer.Deserialize<Users>(currentUserInFile);
                    if (user.id == id)
                        textToReplace = currentUserInFile;
                }
            }

            if (textToReplace != string.Empty)
            {
                string text = System.IO.File.ReadAllText(filePath);
                text = text.Replace(textToReplace, JsonSerializer.Serialize(userToUpdate));
                System.IO.File.WriteAllText(filePath, text);
            }
        }
    }
}
