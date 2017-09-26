using System;

namespace CookMasterApiModel
{
    public class Cooker
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public Guid Token { get; set; }
    }
}