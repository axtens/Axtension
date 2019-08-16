using System;
using System.Collections.Generic;
using System.Text;

namespace Axtension
{
	public class Stopwatch
	{
		private DateTime _start;
		private DateTime _stop;
		public Stopwatch()
		{
			this._start = new DateTime();
			this._stop = new DateTime();
		}
		public void clear()
		{
			this._start = new DateTime();
			this._stop = new DateTime();
		}
		public void start()
		{
			this._start = DateTime.Now;
		}
		public void stop()
		{
			this._stop = DateTime.Now;
		}
		public long split()
		{
			return DateTime.Now.Ticks - this._start.Ticks;
		}
		public double minutes()
		{
			TimeSpan t = TimeSpan.FromTicks(this.split());
			return t.TotalMinutes;
		}
		public double seconds()
		{
			TimeSpan t = TimeSpan.FromTicks(this.split());
			return t.TotalSeconds;
		}
		public double milliseconds()
		{
			TimeSpan t = TimeSpan.FromTicks(this.split());
			return t.TotalMilliseconds;
		}

	}

}
