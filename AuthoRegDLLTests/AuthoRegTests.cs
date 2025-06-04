using AuthoRegDLL;
using Npgsql;
using System.Text;
namespace AuthoRegDLLTests
{
    /// <summary>
    /// Тестовый класс для проверки функциональности аутентификации и регистрации.
    /// Содержит модульные тесты для проверки валидации регистрации, регистрации пользователей, аутентификации и хеширования паролей.
    /// </summary>
    [TestClass]
    public sealed class AuthoRegTests
    {
        private const string TestLogin = "testuser";
        private const string TestPassword = "Pass1!";
        private const string TestFullName = "Иванов Иван Иванович";
        private static readonly string ConnectionString = "Server=localhost;Port=5432;UserId=postgres;Password=123;Database=advancenotesmanager";

        /// <summary>
        /// Метод инициализации класса, выполняемый перед запуском тестов.
        /// Очищает тестовых пользователей из базы данных и добавляет тестового пользователя.
        /// </summary>
        /// <param name="context">Контекст теста, предоставляемый MSTest.</param>
        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            using var conn = new NpgsqlConnection(ConnectionString);
            conn.Open();
            // Очистка тестовых пользователей
            using var del = new NpgsqlCommand(
                "DELETE FROM users_data WHERE login IN ('testuser', 'worker1', 'worker2')", conn);
            del.ExecuteNonQuery();
            // Вставка тестового пользователя
            using var insert = new NpgsqlCommand(
                "INSERT INTO users_data (login, hashed_password, role, full_name, is_director) " +
                "VALUES (@login, @hashed_password, @role, @full_name, @is_director)", conn);
            insert.Parameters.AddWithValue("login", TestLogin);
            insert.Parameters.AddWithValue("hashed_password", ComputeHash(TestPassword));
            insert.Parameters.AddWithValue("role", "director");
            insert.Parameters.AddWithValue("full_name", TestFullName);
            insert.Parameters.AddWithValue("is_director", true);
            insert.ExecuteNonQuery();
        }

        // Тесты для метода ValidateRegistration
        /// <summary>
        /// Тест TC1: Проверка валидации регистрации с корректными входными данными.
        /// Ожидается, что данные пройдут валидацию без ошибок.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        public void TC1_ValidateRegistration_ValidInput()
        {
            var authoReg = new AuthoReg();
            var (isValid, errorMessage) = authoReg.ValidateRegistration("user123", TestPassword, TestFullName, false);
            Assert.IsTrue(isValid, "Ожидалось, что корректные данные будут валидными");
            Assert.AreEqual("", errorMessage);
        }

        /// <summary>
        /// Тест TC2: Проверка валидации регистрации с пустым логином.
        /// Ожидается, что пустой логин будет признан невалидным.
        /// </summary>
        [TestMethod]
        [Priority(2)]
        public void TC2_ValidateRegistration_EmptyLogin()
        {
            var authoReg = new AuthoReg();
            var (isValid, errorMessage) = authoReg.ValidateRegistration("", TestPassword, TestFullName, false);
            Assert.IsFalse(isValid, "Ожидалось, что пустой логин будет невалидным");
            Assert.AreEqual("Логин не может быть пустым", errorMessage);
        }

        /// <summary>
        /// Тест TC3: Проверка валидации регистрации с коротким логином (менее 5 символов).
        /// Ожидается, что логин будет признан невалидным.
        /// </summary>
        [TestMethod]
        [Priority(2)]
        public void TC3_ValidateRegistration_ShortLogin()
        {
            var authoReg = new AuthoReg();
            var (isValid, errorMessage) = authoReg.ValidateRegistration("user", TestPassword, TestFullName, false);
            Assert.IsFalse(isValid, "Ожидалось, что короткий логин (менее 5 символов) будет невалидным");
            Assert.AreEqual("Логин должен содержать от 5 до 10 символов", errorMessage);
        }

        /// <summary>
        /// Тест TC4: Проверка валидации регистрации с недопустимыми символами в логине.
        /// Ожидается, что логин с символами, не соответствующими требованиям, будет невалидным.
        /// </summary>
        [TestMethod]
        [Priority(2)]
        public void TC4_ValidateRegistration_InvalidLoginCharacters()
        {
            var authoReg = new AuthoReg();
            var (isValid, errorMessage) = authoReg.ValidateRegistration("!@#$%", TestPassword, TestFullName, false);
            Assert.IsFalse(isValid, "Ожидалось, что логин с недопустимыми символами будет невалидным");
            Assert.AreEqual("Логин может содержать только латинские буквы, цифры и подчеркивания", errorMessage);
        }

        /// <summary>
        /// Тест TC5: Проверка валидации регистрации с уже существующим логином.
        /// Ожидается, что повторный логин будет признан невалидным.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        public void TC5_ValidateRegistration_ExistingLogin()
        {
            var authoReg = new AuthoReg();
            var (isValid, errorMessage) = authoReg.ValidateRegistration(TestLogin, TestPassword, TestFullName, false);
            Assert.IsFalse(isValid, "Ожидалось, что существующий логин будет невалидным");
            Assert.AreEqual("Логин уже существует", errorMessage);
        }

        /// <summary>
        /// Тест TC6: Проверка валидации регистрации с пустым паролем.
        /// Ожидается, что пустой пароль будет признан невалидным.
        /// </summary>
        [TestMethod]
        [Priority(2)]
        public void TC6_ValidateRegistration_EmptyPassword()
        {
            var authoReg = new AuthoReg();
            var (isValid, errorMessage) = authoReg.ValidateRegistration("user123", "", TestFullName, false);
            Assert.IsFalse(isValid, "Ожидалось, что пустой пароль будет невалидным");
            Assert.AreEqual("Пароль не может быть пустым", errorMessage);
        }

        /// <summary>
        /// Тест TC7: Проверка валидации регистрации с коротким паролем (менее 3 символов).
        /// Ожидается, что короткий пароль будет признан невалидным.
        /// </summary>
        [TestMethod]
        [Priority(2)]
        public void TC7_ValidateRegistration_ShortPassword()
        {
            var authoReg = new AuthoReg();
            var (isValid, errorMessage) = authoReg.ValidateRegistration("user123", "P1", TestFullName, false);
            Assert.IsFalse(isValid, "Ожидалось, что короткий пароль (менее 3 символов) будет невалидным");
            Assert.AreEqual("Пароль должен содержать от 3 до 8 символов", errorMessage);
        }

        /// <summary>
        /// Тест TC8: Проверка валидации регистрации с паролем, не соответствующим формату.
        /// Ожидается, что пароль без заглавной буквы и спецсимвола будет невалидным.
        /// </summary>
        [TestMethod]
        [Priority(2)]
        public void TC8_ValidateRegistration_InvalidPasswordFormat()
        {
            var authoReg = new AuthoReg();
            var (isValid, errorMessage) = authoReg.ValidateRegistration("user123", "pass123", TestFullName, false);
            Assert.IsFalse(isValid, "Ожидалось, что пароль без заглавной буквы и спецсимвола будет невалидным");
            Assert.AreEqual("Пароль должен содержать как минимум одну заглавную букву, одну цифру и один специальный символ", errorMessage);
        }

        /// <summary>
        /// Тест TC9: Проверка валидации регистрации с пустым ФИО.
        /// Ожидается, что пустое ФИО будет признано невалидным.
        /// </summary>
        [TestMethod]
        [Priority(2)]
        public void TC9_ValidateRegistration_EmptyFullName()
        {
            var authoReg = new AuthoReg();
            var (isValid, errorMessage) = authoReg.ValidateRegistration("user123", TestPassword, "", false);
            Assert.IsFalse(isValid, "Ожидалось, что пустое ФИО будет невалидным");
            Assert.AreEqual("Полное ФИО не может быть пустым", errorMessage);
        }

        /// <summary>
        /// Тест TC10: Проверка валидации регистрации с слишком длинным ФИО.
        /// Ожидается, что ФИО длиннее 50 символов будет невалидным.
        /// </summary>
        [TestMethod]
        [Priority(2)]
        public void TC10_ValidateRegistration_LongFullName()
        {
            var longName = new string('А', 51);
            var authoReg = new AuthoReg();
            var (isValid, errorMessage) = authoReg.ValidateRegistration("user123", TestPassword, longName, false);
            Assert.IsFalse(isValid, "Ожидалось, что слишком длинное ФИО будет невалидным");
            Assert.AreEqual("Полное ФИО не может превышать 50 символов", errorMessage);
        }

        /// <summary>
        /// Тест TC11: Проверка валидации регистрации с ФИО, состоящим из двух частей.
        /// Ожидается, что ФИО из двух частей будет невалидным.
        /// </summary>
        [TestMethod]
        [Priority(2)]
        public void TC11_ValidateRegistration_InvalidFullNameParts()
        {
            var authoReg = new AuthoReg();
            var (isValid, errorMessage) = authoReg.ValidateRegistration("user123", TestPassword, "Иванов Иван", false);
            Assert.IsFalse(isValid, "Ожидалось, что ФИО из двух частей будет невалидным");
            Assert.AreEqual("Полное ФИО должно состоять из трех частей: Фамилия, Имя, Отчество", errorMessage);
        }

        /// <summary>
        /// Тест TC12: Проверка валидации регистрации с ФИО, содержащим латинские буквы.
        /// Ожидается, что ФИО с латинскими буквами будет невалидным.
        /// </summary>
        [TestMethod]
        [Priority(3)]
        public void TC12_ValidateRegistration_NonCyrillicFullName()
        {
            var authoReg = new AuthoReg();
            var (isValid, errorMessage) = authoReg.ValidateRegistration("user123", TestPassword, "Ivanov Ivan Ivanovich", false);
            Assert.IsFalse(isValid, "Ожидалось, что ФИО с латинскими буквами будет невалидным");
            Assert.AreEqual("Полное ФИО может содержать только кириллические буквы и пробелы", errorMessage);
        }

        /// <summary>
        /// Тест TC13: Проверка валидации регистрации с ФИО, не начинающимся с заглавных букв.
        /// Ожидается, что ФИО с маленькими буквами будет невалидным.
        /// </summary>
        [TestMethod]
        [Priority(3)]
        public void TC13_ValidateRegistration_NonCapitalizedFullName()
        {
            var authoReg = new AuthoReg();
            var (isValid, errorMessage) = authoReg.ValidateRegistration("user123", TestPassword, "иванов иван иванович", false);
            Assert.IsFalse(isValid, "Ожидалось, что ФИО с маленькими буквами будет невалидным");
            Assert.AreEqual("Каждая часть ФИО должна начинаться с заглавной буквы", errorMessage);
        }

        // Тесты для метода Register
        /// <summary>
        /// Тест TC14: Проверка регистрации с корректными входными данными.
        /// Ожидается успешная регистрация сотрудника.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        public void TC14_Register_ValidInput()
        {
            var authoReg = new AuthoReg();
            var (success, errorMessage) = authoReg.Register("worker1", TestPassword, "Колончак Петр Константинович", false);
            Assert.IsTrue(success, "Ожидалось, что регистрация сотрудника пройдет успешно");
            Assert.AreEqual("", errorMessage);
        }

        /// <summary>
        /// Тест TC15: Проверка регистрации с некорректным логином.
        /// Ожидается, что регистрация с коротким логином завершится ошибкой.
        /// </summary>
        [TestMethod]
        [Priority(2)]
        public void TC15_Register_InvalidInput()
        {
            var authoReg = new AuthoReg();
            var (success, errorMessage) = authoReg.Register("user", TestPassword, TestFullName, false);
            Assert.IsFalse(success, "Ожидалось, что регистрация с коротким логином завершится ошибкой");
            Assert.AreEqual("Логин должен содержать от 5 до 10 символов", errorMessage);
        }

        /// <summary>
        /// Тест TC16: Проверка регистрации с нарушением уникальности логина.
        /// Ожидается, что повторная регистрация завершится ошибкой.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        public void TC16_Register_DatabaseFailure()
        {
            var authoReg = new AuthoReg();
            // Регистрируем пользователя
            authoReg.Register("worker2", TestPassword, "Ефремов Евгений Александрович", false);
            // Повторная регистрация для вызова нарушения уникальности
            var (success, errorMessage) = authoReg.Register("worker2", TestPassword, "Ефремов Евгений Александрович", false);
            Assert.IsFalse(success, "Ожидалось, что повторная регистрация завершится ошибкой из-за существующего логина");
            Assert.AreEqual("Логин уже существует", errorMessage);
        }

        /// <summary>
        /// Тест TC17: Проверка регистрации при превышении лимита директоров.
        /// Ожидается, что регистрация третьего директора завершится ошибкой.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        public void TC17_Register_DirectorLimitExceeded()
        {
            var authoReg = new AuthoReg();        
            var (success, errorMessage) = authoReg.Register("testuser2", TestPassword, "Кравчук Анатолий Генадьевич", true);
            Assert.IsFalse(success, "Ожидалось, что регистрация третьего директора завершится ошибкой");
            Assert.AreEqual("Нельзя назначить более двух руководителей", errorMessage);
        }

        // Тесты для метода Authenticate
        /// <summary>
        /// Тест TC18: Проверка аутентификации с корректными учетными данными.
        /// Ожидается, что пользователь будет успешно аутентифицирован.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        public void TC18_Authenticate_ValidCredentials()
        {
            var authoReg = new AuthoReg();
            var (user, errorMessage) = authoReg.Authenticate(TestLogin, TestPassword);
            Assert.IsNotNull(user, "Ожидалось, что существующий пользователь с правильным паролем вернет объект User");
            Assert.AreEqual(TestLogin, user.Login, "Логин пользователя должен соответствовать ожидаемому");
            Assert.AreEqual("director", user.Role, "Роль пользователя должна быть 'director'");
            Assert.AreEqual("", errorMessage);
        }

        /// <summary>
        /// Тест TC19: Проверка аутентификации с пустыми учетными данными.
        /// Ожидается, что пустые логин и пароль вернут ошибку.
        /// </summary>
        [TestMethod]
        [Priority(2)]
        public void TC19_Authenticate_EmptyCredentials()
        {
            var authoReg = new AuthoReg();
            var (user, errorMessage) = authoReg.Authenticate("", "");
            Assert.IsNull(user, "Ожидалось, что пустые логин и пароль вернут null");
            Assert.AreEqual("Логин и пароль не могут быть пустыми", errorMessage);
        }

        /// <summary>
        /// Тест TC20: Проверка аутентификации с неверным логином.
        /// Ожидается, что несуществующий логин вернет ошибку.
        /// </summary>
        [TestMethod]
        [Priority(2)]
        public void TC20_Authenticate_WrongLogin()
        {
            var authoReg = new AuthoReg();
            var (user, errorMessage) = authoReg.Authenticate("nouser", TestPassword);
            Assert.IsNull(user, "Ожидалось, что несуществующий пользователь вернет null");
            Assert.AreEqual("Неверный логин или пароль", errorMessage);
        }

        /// <summary>
        /// Тест TC21: Проверка аутентификации с неверным паролем.
        /// Ожидается, что неверный пароль вернет ошибку.
        /// </summary>
        [TestMethod]
        [Priority(2)]
        public void TC21_Authenticate_WrongPassword()
        {
            var authoReg = new AuthoReg();
            var (user, errorMessage) = authoReg.Authenticate(TestLogin, "WrongPass1!");
            Assert.IsNull(user, "Ожидалось, что неправильный пароль вернет null");
            Assert.AreEqual("Неверный логин или пароль", errorMessage);
        }

        // Тесты для метода HashPassword
        /// <summary>
        /// Тест TC22: Проверка хеширования одинаковых паролей.
        /// Ожидается, что одинаковые пароли дают одинаковый хеш.
        /// </summary>
        [TestMethod]
        [Priority(3)]
        public void TC22_HashPassword_SameInput()
        {
            var authoReg = new AuthoReg();
            string password = TestPassword;
            string hash1 = ComputeHash(password);
            string hash2 = ComputeHash(password);
            Assert.AreEqual(hash1, hash2, "Ожидалось, что одинаковые пароли дают одинаковый хэш");
        }

        /// <summary>
        /// Тест TC23: Проверка хеширования разных паролей.
        /// Ожидается, что разные пароли дают разные хеши.
        /// </summary>
        [TestMethod]
        [Priority(3)]
        public void TC23_HashPassword_DifferentInput()
        {
            var authoReg = new AuthoReg();
            string hash1 = ComputeHash("Pass1!");
            string hash2 = ComputeHash("Pass2!");
            Assert.AreNotEqual(hash1, hash2, "Ожидалось, что разные пароли дают разные хэши");
        }

        /// <summary>
        /// Вспомогательный метод для вычисления SHA256-хеша пароля.
        /// Преобразует входную строку в байты, вычисляет хеш и возвращает его в виде шестнадцатеричной строки.
        /// </summary>
        /// <param name="password">Пароль для хеширования.</param>
        /// <returns>Хеш пароля в виде строки.</returns>
        private static string ComputeHash(string password)
        {
            using (System.Security.Cryptography.SHA256 sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hash)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}

