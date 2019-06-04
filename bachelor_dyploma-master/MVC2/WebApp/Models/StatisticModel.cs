using System;
using System.ComponentModel.DataAnnotations;
using DAL;

namespace WebApp.Models
{
    public class StatisticModel
    {
        public int Id { get; set; }
        public int Indicator { get; set; }
        public int MonthAmount { get; set; }
    }
}