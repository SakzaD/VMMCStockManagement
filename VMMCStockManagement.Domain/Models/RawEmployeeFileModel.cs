using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMMCStockManagement.Domain.Entities;

namespace VMMCStockManagement.Domain.Models
{
	public class RawEmployeeFileModel
	{
		public string Grant { get; set; }
		public string Province { get; set; }
		public string EmpCode { get; set; }
		public string EmpName { get; set; }
		public string Initials { get; set; }
		public string KnownAsName { get; set; }
		public string FirstName { get; set; }
		public string SecondName { get; set; }
		public string LastName { get; set; }
		public string EEStatus { get; set; }
		public string EmpCategory { get; set; }
		public DateTime? DateJoinedGroup { get; set; }
		public DateTime? DateEngaged { get; set; }
		public DateTime? ContractStart { get; set; }
		public DateTime? ContractEnd { get; set; }
		public DateTime? LveStartDate { get; set; }
		public double TotalLveDue { get; set; }
		public int Age { get; set; }
		public double YearsOfService { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public string IdNumber { get; set; }
		public string PassportNumber { get; set; }
		public string PassportCountry { get; set; }
		public bool IsForeignNational { get; set; }
		public Gender Gender { get; set; }
		public string Race { get; set; }
		public string Disability { get; set; }
		public string NatureOfDisability { get; set; }
		public string JobTitle { get; set; }
		public DateTime? TerminationDate { get; set; }
		public string TerminationReason { get; set; }
		public bool DoNotReEmploy { get; set; }
		public string Zone { get; set; }
		public string PayPoint { get; set; }
		public string ManagerEmpId { get; set; }
		public string ManagerName { get; set; }
		public string EmailAddress { get; set; }
		public string HridHierachyName { get; set; }

	}
}
