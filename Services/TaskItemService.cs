using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using SimpleAPI.Data;

namespace SimpleAPI.Models
{
    public class TaskItemService
    {

        private readonly SQLiteContext _context;

        public TaskItemService(SQLiteContext context)
        {
            _context = context;
        }


       


    }
}
