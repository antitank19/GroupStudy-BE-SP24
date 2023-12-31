using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareResource.DTO
{
    public class StatGetDto
    {
        public string StudentUsername { get; set; }
        public string StudentFullname { get; set; }
        public DateTime Month { get; set; }
        //public IQueryable TotalMeetings { get; set; }
        public int TotalMeetingsCount { get; set; }
        public IQueryable AtendedMeetings { get; set; }
        public int AtendedMeetingsCount { get; set; }
        public int MissedMeetingsCount { get; set; }
        public string TotalMeetingTme { get; set; }
        public double AverageVoteResult { get; set; } 
        public string ToHtml()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Thông tin chi tiết việc học tháng: {Month.ToString("MM/yyyy")}");
            sb.Append($"của học sinh: {StudentFullname}, {StudentUsername}");
            sb.AppendLine($"Tổng số buổi học: {TotalMeetingsCount}");
            sb.AppendLine($"Số buổi học tham gia: {AtendedMeetingsCount}");
            sb.AppendLine($"Số buổi học vắng: {MissedMeetingsCount}");
            sb.AppendLine($"Tổng thời gian học: {TotalMeetingTme}");
            sb.AppendLine($"Trung bình điểm trả bài: {AverageVoteResult}");
            return sb.ToString();
        }
    }
    public class StatGetListDto
    {
        public string StudentUsername { get; set; }
        public string StudentFullname { get; set; }
        public DateTime Month { get; set; }
        //public IQueryable TotalMeetings { get; set; }
        public int TotalMeetingsCount { get; set; }
        public int AtendedMeetingsCount { get; set; }
        public int MissedMeetingsCount { get; set; }
        public TimeSpan TotalMeetingTme { get; set; }
        public double AverageVoteResult { get; set; }
        public string ToHtml()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Thông tin chi tiết việc học tháng: {Month.ToString("MM/yyyy")}");
            sb.Append($"của học sinh: {StudentFullname}, {StudentUsername}");
            sb.AppendLine($"Tổng số buổi học: {TotalMeetingsCount}");
            sb.AppendLine($"Số buổi học tham gia: {AtendedMeetingsCount}");
            sb.AppendLine($"Số buổi học vắng: {MissedMeetingsCount}");
            sb.AppendLine($"Tổng thời gian học: {TotalMeetingTme}");
            sb.AppendLine($"Trung bình điểm trả bài: {AverageVoteResult}");
            return sb.ToString();
        }
    }
}
