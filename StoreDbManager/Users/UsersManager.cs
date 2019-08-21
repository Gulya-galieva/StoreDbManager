using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace DbManager
{
    /// <summary>
    /// Препоставляет методы для работы с Таблицей пользователей в БД
    /// </summary>
    public class UsersManager
    {
        StoreContext db;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public UsersManager(StoreContext context)
        {
            db = context;
        }

        /// <summary>
        /// Добавляет нового пользователя в БД
        /// </summary>
        /// <param name="user">Инфа о новом пользователе</param>
        public void AddUser(User user)
        {
            user.Password = GetMd5Hash(MD5.Create(), user.Password);
            db.Users.Add(user);
            db.SaveChanges();
        }

        /// <summary>
        /// Удаляем пользователя из БД по Id
        /// </summary>
        /// <param name="userId">Id пользователя, которого нужно удалить из БД</param>
        public void DeleteUser(int userId)
        {
            var user = db.Users.Find(userId);
            //Delete linked comments and unread flags
            var data = from u in db.UnreadSubstationComments
                       join c in db.CommentSubstations on u.CommentSubstationId equals c.Id
                       where c.UserId == user.Id
                       select new { u, c };
            db.UnreadSubstationComments.RemoveRange(data.Select(d => d.u));
            db.CommentSubstations.RemoveRange(data.Select(d => d.c));
            //Delete user
            db.Users.Remove(user);
            db.SaveChanges();
        }

        //Обновить инфу
        /// <summary>
        /// Изменить инфу пользователя в БД (Логин, e-mail, Имя).
        /// </summary>
        /// <param name="userId">Id пользователя, данные которого нужно изменить в БД</param>
        /// <param name="login">Новый Логин (используется для входа в систему)</param>
        /// <param name="eMail">Новый e-mail</param>
        /// <param name="name">Новое Имя пользователя (не логин, просто инфо)</param>
        public void UpdateUserInfo(int userId, string login, string eMail, string name)
        {
            var userOld = db.Users.Find(userId);
            if(userOld != null)
            {
                if (login != null) userOld.Login = login;
                if (eMail != null) userOld.Email = eMail;
                if (name != null) userOld.Name = name;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Установить новый пароль
        /// </summary>
        /// <param name="userId">Id пользователя, данные которого нужно изменить в БД</param>
        /// <param name="newPassword">Новый пароль</param>
        public void UpdatePass(int userId, string newPassword)
        {
            var userOld = db.Users.Find(userId);
            if (userOld != null)
            {
                if (newPassword != null)
                    userOld.Password = GetMd5Hash(MD5.Create(), newPassword);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId">Id пользователя, данные которого нужно изменить в БД</param>
        /// <param name="newRoleId">Id новой роли</param>
        public void UpdateRole(int userId, int newRoleId)
        {
            var user = db.Users.Find(userId);
            var role = db.Roles.Find(newRoleId);

            if (user != null && role != null)
            {
                user.RoleId = newRoleId;
                db.SaveChanges();
            }
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}
