using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignTrack.Models;
using Microsoft.EntityFrameworkCore;

namespace SignTrack.Services
{
    public class StudentService
    {
        

        internal static IQueryable<Student> QGetByExample(SignContext context, Student stu)
        {
            return context.Students
                        .Where(x => x.Name == stu.Name && x.Phone == stu.Phone && x.StuNo == stu.StuNo)
                        .Include("SignIns");

        }
    }
}
