using System;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public static class UserSession
    {
        public static int UserID { get; set; }
        public static string Username { get; set; }
        public static string Role { get; set; }
        public static int? LinkedStudentID { get; set; }
        public static int? LinkedLecturerID { get; set; }
        public static bool IsLoggedIn { get; set; }

        public static void StartSession(int userID, string username, string role, int? linkedStudentID = null, int? linkedLecturerID = null)
        {
            UserID = userID;
            Username = username;
            Role = role;
            LinkedStudentID = linkedStudentID;
            LinkedLecturerID = linkedLecturerID;
            IsLoggedIn = true;
        }

        public static void EndSession()
        {
            UserID = 0;
            Username = string.Empty;
            Role = string.Empty;
            LinkedStudentID = null;
            LinkedLecturerID = null;
            IsLoggedIn = false;
        }

        public static bool IsAdmin()
        {
            string role = Role?.ToLower().Trim();
            return role == "admin" || role == "quản trị" || role == "quantri";
        }

        public static bool IsStudent()
        {
            string role = Role?.ToLower().Trim();
            return role == "student" || role == "sinh viên" || role == "sinhvien";
        }

        public static bool IsLecturer()
        {
            string role = Role?.ToLower().Trim();
            return role == "lecturer" || role == "giảng viên" || role == "giangvien";
        }
    }
}