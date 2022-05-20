using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongOnlineWorker.Models
{
	public class MqttMessageDTO
	{
		public string Topic { get; set; }
		public string Message { get; set; }
	}
}
