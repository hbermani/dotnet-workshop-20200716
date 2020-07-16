using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autozone.Website.Models;

namespace Autozone.Website.Data {
	public class DuplicateCarException : Exception {

		public DuplicateCarException(string message) : base(message) {

		}
	}
}
