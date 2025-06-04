using DataBaseDLL;
using Npgsql;

namespace DataBaseDLLTests
{
    /// <summary>
    /// Класс для тестирования функциональности базы данных в приложении AdvanceNotesManager.
    /// Содержит юнит-тесты для проверки операций с пользователями и заметками.
    /// </summary>
    [TestClass]
    public sealed class DataBaseTests
    {
        private const string TestLogin = "testuser";
        private const string TestPassword = "Pass1!";
        private const string TestFullName = "Иванов Иван Иванович";
        private static readonly string ConnectionString = "Server=localhost;Port=5432;UserId=postgres;Password=123;Database=advancenotesmanager";
        private Database db;

        /// <summary>
        /// Метод инициализации, выполняющийся один раз перед запуском всех тестов в классе.
        /// Очищает тестовые данные и добавляет тестового пользователя (директора) в базу.
        /// </summary>
        [ClassInitialize]
        public static void ClassSetup(TestContext context)
        {
            using var conn = new NpgsqlConnection(ConnectionString);
            conn.Open();
            // Очистка тестовых данных
            using var del = new NpgsqlCommand(
                "DELETE FROM notes_data WHERE title LIKE 'TestNote%'; " +
                "DELETE FROM users_data WHERE login IN ('testuser', 'worker3', 'director1')", conn);
            del.ExecuteNonQuery();

            // Вставка тестового пользователя (директора)
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

        /// <summary>
        /// Метод инициализации, выполняющийся перед каждым тестом.
        /// Создает новый экземпляр класса Database для теста.
        /// </summary>
        [TestInitialize]
        public void TestSetup()
        {
            db = new Database();
        }

        /// <summary>
        /// Тест TC1: Проверка существования пользователя с существующим логином.
        /// Ожидается, что пользователь будет найден.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        public void TC1_UserExists_ExistingUser()
        {
            bool exists = db.UserExists(TestLogin);
            Assert.IsTrue(exists, "Ожидалось, что существующий пользователь будет найден");
        }

        /// <summary>
        /// Тест TC2: Проверка существования пользователя с несуществующим логином.
        /// Ожидается, что пользователь не будет найден.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        public void TC2_UserExists_NonExistingUser()
        {
            bool exists = db.UserExists("nonexistent");
            Assert.IsFalse(exists, "Ожидалось, что несуществующий пользователь не будет найден");
        }

        /// <summary>
        /// Тест TC3: Получение данных существующего пользователя по логину.
        /// Проверяет, что возвращаются корректные данные (логин, ФИО, статус директора).
        /// </summary>
        [TestMethod]
        [Priority(1)]
        public void TC3_GetUserByLogin_ExistingUser()
        {
            var user = db.GetUserByLogin(TestLogin);
            Assert.IsNotNull(user, "Ожидалось, что данные существующего пользователя будут возвращены");
            Assert.AreEqual(TestLogin, user?.login, "Логин должен соответствовать ожидаемому");
            Assert.AreEqual(TestFullName, user?.fullName, "ФИО должно соответствовать ожидаемому");
            Assert.IsTrue(user?.isDirector, "Пользователь должен быть директором");
        }

        /// <summary>
        /// Тест TC4: Получение данных несуществующего пользователя по логину.
        /// Ожидается, что метод вернет null.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        public void TC4_GetUserByLogin_NonExistingUser()
        {
            var user = db.GetUserByLogin("nonexistent");
            Assert.IsNull(user, "Ожидалось, что для несуществующего пользователя вернется null");
        }

        /// <summary>
        /// Тест TC5: Добавление нового сотрудника с уникальным логином.
        /// Проверяет успешность добавления и наличие пользователя в базе.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        public void TC5_InsertUser_ValidEmployee()
        {
            bool success = db.InsertUser("worker3", ComputeHash(TestPassword), "Петров Петр Петрович", false);
            Assert.IsTrue(success, "Ожидалась успешная регистрация сотрудника");
            Assert.IsTrue(db.UserExists("worker3"), "Пользователь должен быть добавлен в базу");
        }

        /// <summary>
        /// Тест TC6: Попытка добавления пользователя с уже существующим логином.
        /// Ожидается, что регистрация завершится неудачей.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        public void TC6_InsertUser_DuplicateLogin()
        {
            db.InsertUser("worker3", ComputeHash(TestPassword), "Петров Петр Петрович", false);
            bool success = db.InsertUser("worker3", ComputeHash(TestPassword), "Сидоров Сидор Сидорович", false);
            Assert.IsFalse(success, "Ожидалось, что регистрация с дублирующимся логином завершится неудачей");
        }

        /// <summary>
        /// Тест TC7: Попытка добавления второго директора, что превышает лимит.
        /// Ожидается исключение с сообщением о превышении лимита директоров.
        /// </summary>
        [TestMethod]
        [Priority(2)]
        [ExpectedException(typeof(Exception), "Нельзя назначить более двух руководителей")]
        public void TC7_InsertUser_DirectorLimitExceeded()
        {
            db.InsertUser("director1", ComputeHash(TestPassword), "Сидоров Сидор Сидорович", true);
        }

        /// <summary>
        /// Тест TC8: Добавление корректной заметки.
        /// Проверяет, что заметка успешно добавлена в базу.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        public void TC8_AddNote_ValidNote()
        {
            int directorId = GetUserId(TestLogin);
            bool success = db.AddNote("TestNote1", "Description", "Высокий", 2, new DateTime(2025, 6, 10), directorId);
            Assert.IsTrue(success, "Ожидалось, что заметка будет успешно добавлена");
        }

        /// <summary>
        /// Тест TC9: Попытка назначения директора исполнителем заметки.
        /// Ожидается исключение, так как директор не может быть исполнителем.
        /// </summary>
        [TestMethod]
        [Priority(2)]
        [ExpectedException(typeof(Exception), "Руководитель не может быть испонителем")]
        public void TC9_AddNote_DirectorAsAssignee()
        {
            int directorId = GetUserId(TestLogin);
            db.AddNote("TestNote2", "Description", "Высокий", directorId, new DateTime(2025, 6, 10), directorId);
        }

        /// <summary>
        /// Тест TC10: Попытка добавления заметки с некорректным приоритетом.
        /// Ожидается исключение PostgresException из-за неверного значения приоритета.
        /// </summary>
        [TestMethod]
        [Priority(2)]
        [ExpectedException(typeof(PostgresException), "Невалидное значение для приоритета или статуса")]
        public void TC10_AddNote_InvalidPriority()
        {
            int directorId = GetUserId(TestLogin);
            db.AddNote("TestNote3", "Description", "Invalid", 2, new DateTime(2025, 6, 10), directorId);
        }

        /// <summary>
        /// Тест TC11: Обновление существующей заметки с корректными данными.
        /// Проверяет успешность обновления заметки.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        public void TC11_UpdateNote_ValidUpdate()
        {
            int directorId = GetUserId(TestLogin);
            db.AddNote("TestNote4", "Old Description", "Низкий", 2, new DateTime(2025, 6, 10), directorId);
            int noteId = GetNoteId("TestNote4");
            bool success = db.UpdateNote(noteId, "Updated Note4", "New Description", "Высокий", "Отказано", 2, new DateTime(2025, 6, 10));
            Assert.IsTrue(success, "Ожидалось, что заметка будет успешно обновлена");
        }

        /// <summary>
        /// Тест TC12: Попытка обновления заметки с некорректным приоритетом.
        /// Ожидается исключение PostgresException из-за неверного значения приоритета.
        /// </summary>
        [TestMethod]
        [Priority(2)]
        [ExpectedException(typeof(PostgresException), "Невалидное значение для приоритета или статуса")]
        public void TC12_UpdateNote_InvalidPriority()
        {
            int directorId = GetUserId(TestLogin);
            db.AddNote("TestNote5", "Description", "Низкий", 2, new DateTime(2025, 6, 10), directorId);
            int noteId = GetNoteId("TestNote5");
            db.UpdateNote(noteId, "Updated Note5", "Description", "Invalid", "Отказано", 2, new DateTime(2025, 6, 10));
        }

        /// <summary>
        /// Тест TC13: Удаление существующей заметки.
        /// Проверяет успешность удаления заметки.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        public void TC13_DeleteNote_ValidNote()
        {
            int directorId = GetUserId(TestLogin);
            db.AddNote("TestNote6", "Description", "Низкий", 2, new DateTime(2025, 6, 10), directorId);
            int noteId = GetNoteId("TestNote6");
            bool success = db.DeleteNote(noteId);
            Assert.IsTrue(success, "Ожидалось, что заметка будет успешно удалена");
        }

        /// <summary>
        /// Тест TC14: Получение отфильтрованных заметок для директора.
        /// Проверяет, что возвращаются заметки, соответствующие фильтрам.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        public void TC14_GetFilteredNotesForDirector_ValidFilters()
        {
            int directorId = GetUserId(TestLogin);
            db.AddNote("TestNote7", "Description", "Высокий", 2, new DateTime(2025, 6, 10), directorId);
            var dt = db.GetFilteredNotesForDirector(directorId, "В работе", "Высокий", 2);
            Assert.IsTrue(dt.Rows.Count > 0, "Ожидалось, что будут возвращены отфильтрованные заметки");
        }

        /// <summary>
        /// Тест TC15: Получение отфильтрованных заметок для сотрудника.
        /// Проверяет, что возвращаются заметки, соответствующие фильтрам.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        public void TC15_GetFilteredNotesForWorker_ValidFilters()
        {
            int directorId = GetUserId(TestLogin);
            db.AddNote("TestNote8", "Description", "Средний", 2, new DateTime(2025, 6, 10), directorId);
            int noteId = GetNoteId("TestNote8");
            var dt = db.GetFilteredNotesForWorker(noteId, "В работе", "Средний", 2);
            Assert.IsTrue(dt.Rows.Count > 0, "Ожидалось, что будут возвращены отфильтрованные заметки для сотрудника");
        }

        /// <summary>
        /// Тест TC16: Обновление статуса заметки с корректным значением.
        /// Проверяет успешность обновления статуса.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        public void TC16_UpdateNoteStatus_ValidStatus()
        {
            int directorId = GetUserId(TestLogin);
            db.AddNote("TestNote9", "Description", "Низкий", 2, new DateTime(2025, 6, 10), directorId);
            int noteId = GetNoteId("TestNote9");
            bool success = db.UpdateNoteStatus(noteId, "Выполнено");
            Assert.IsTrue(success, "Ожидалось, что статус заметки будет успешно обновлен");
        }

        /// <summary>
        /// Тест TC17: Попытка обновления статуса заметки с некорректным значением.
        /// Ожидается исключение PostgresException из-за неверного значения статуса.
        /// </summary>
        [TestMethod]
        [Priority(2)]
        [ExpectedException(typeof(PostgresException), "Невалидное значение для статуса")]
        public void TC17_UpdateNoteStatus_InvalidStatus()
        {
            int directorId = GetUserId(TestLogin);
            db.AddNote("TestNote10", "Description", "Низкий", 2, new DateTime(2025, 6, 10), directorId);
            int noteId = GetNoteId("TestNote10");
            db.UpdateNoteStatus(noteId, "Invalid");
        }

        /// <summary>
        /// Вспомогательный метод для хеширования пароля с использованием SHA256.
        /// </summary>
        /// <param name="password">Пароль для хеширования.</param>
        /// <returns>Хешированная строка пароля.</returns>
        private static string ComputeHash(string password)
        {
            using (System.Security.Cryptography.SHA256 sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                System.Text.StringBuilder builder = new System.Text.StringBuilder();
                foreach (byte b in hash)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        /// <summary>
        /// Вспомогательный метод для получения ID пользователя по логину.
        /// </summary>
        /// <param name="login">Логин пользователя.</param>
        /// <returns>ID пользователя.</returns>
        private int GetUserId(string login)
        {
            using var conn = new NpgsqlConnection(ConnectionString);
            conn.Open();
            using var cmd = new NpgsqlCommand("SELECT id FROM users_data WHERE login = @login", conn);
            cmd.Parameters.AddWithValue("login", login);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        /// <summary>
        /// Вспомогательный метод для получения ID заметки по её заголовку.
        /// </summary>
        /// <param name="title">Заголовок заметки.</param>
        /// <returns>ID заметки.</returns>
        private int GetNoteId(string title)
        {
            using var conn = new NpgsqlConnection(ConnectionString);
            conn.Open();
            using var cmd = new NpgsqlCommand("SELECT id FROM notes_data WHERE title = @title", conn);
            cmd.Parameters.AddWithValue("title", title);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
}
