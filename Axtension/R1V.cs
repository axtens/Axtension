using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Axtension
{
	class R1V
	{
		private static string _errorMessage;
		public string ErrorMessage
		{
			set
			{
				_errorMessage = value;
			}
			get
			{
				string temp = _errorMessage;
				_errorMessage = "";
				return temp;
			}
		}
		public R1V()
		{
			_errorMessage = "";
		}
	}
}
