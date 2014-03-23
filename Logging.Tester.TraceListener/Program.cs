
using System;
using System.Diagnostics;
using System.Configuration;

namespace Logging.Tester.TraceListener
{
    class Program
    {
        static void Main( string[] args )
        {
            Trace.WriteLine( "Start" );
            Trace.Indent();
                Trace.WriteLine( "aaaaaaaa" );
                Trace.WriteLine( "aaaaaaaa" );
                Trace.WriteLine( "aaaaaaaa" );
                Trace.Indent();
                    Trace.WriteLine( "bbbbbbbb" );
                    Trace.WriteLine( "bbbbbbbb" );
                    Trace.WriteLine( "bbbbbbbb" );
                Trace.Unindent();
            Trace.Unindent();
            Trace.WriteLine( "End" );

            Console.ReadLine();
        }
    }
}
