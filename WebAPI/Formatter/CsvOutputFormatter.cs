using BusinessObject.Models;
using DataAccess.DTO;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Linq;
using System.Text;
namespace WebAPI.Formatter
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add("text/csv");
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }
        protected override bool CanWriteType(Type? type)
        {
            if (type == typeof(GradeDetailDTO))
            {
                return true;
            }
            else
            {
                Type enumerableType = typeof(IEnumerable<GradeDetailDTO>);
                return enumerableType.IsAssignableFrom(type);
            }
        }
        public async override Task WriteResponseBodyAsync
            (OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var httpContext = context.HttpContext;
            var stringBuilder = new StringBuilder();
            if (context.Object is IEnumerable<GradeDetailDTO> listGrade)
            {
                List<String> title = new List<string>() { "Student Name", "Student Email","Student Code" };
                List<GradeGeneralDTO> gradeGenerals = listGrade.Select(x => x.GradeGeneral).ToList();
                List<AccountDTO> students = listGrade.GroupBy(x => x.StudentCourse.Student.Id).Select(grp => grp.First()).Select(x=>x.StudentCourse.Student).ToList();
                if(gradeGenerals.Any())
                {
                    foreach (var gradeGeneral in gradeGenerals)
                    {
                        if (gradeGeneral != null && gradeGeneral.Assessment != null)
                        {
                            title.Add(gradeGeneral.Assessment.Name+" ("+gradeGeneral.Weight +"%)");
                        }
                    }
                }
                stringBuilder.Append(string.Join(",",title));
                stringBuilder.AppendLine();
                if (students.Any())
                {
                    foreach (var student in students)
                    {
                        List<GradeDetailDTO> list = listGrade.Where(x=>x.StudentCourse.Student.Id == student.Id).ToList();
                        FormatCSV(stringBuilder, list,student);
                    }
                }
            }
            await httpContext.Response.WriteAsync(stringBuilder.ToString(), selectedEncoding);
        }
        private void FormatCSV(StringBuilder buffer, List<GradeDetailDTO> list, AccountDTO acc)
        {
            buffer.Append($"{acc.Name},");
            buffer.Append($"{acc.Email},");
            buffer.Append($"{acc.Code},");
            foreach (var item in list)
            {
                buffer.Append($"{item.Mark},");
            }
            buffer.AppendLine();
        }
    }
}
