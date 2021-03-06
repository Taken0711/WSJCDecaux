﻿using System;
using System.Text;
using System.ServiceModel;
using EventsClient.CalcServiceReference;

namespace EventsClient
{
    class Program
    {
        static void Main(string[] args)
        {

            CalcServiceCallbackSink objsink = new CalcServiceCallbackSink();
            InstanceContext iCntxt = new InstanceContext(objsink);

            CalcServiceClient objClient = new CalcServiceClient(iCntxt);

            objClient.SubscribeValueChanged();

            objClient.ChangeValue(10);
            objClient.ChangeValue(15);

            /*objClient.SubscribeCalculatedEvent ();
            objClient.SubscribeCalculationFinishedEvent ();
            
            double dblNum1 = 1000, dblNum2 = 2000 ;
            objClient.Calculate (0, dblNum1, dblNum2);

            dblNum1 = 2000; dblNum2 = 4000;
            objClient.Calculate(1, dblNum1, dblNum2);

            dblNum1 = 2000; dblNum2 = 4000;
            objClient.Calculate(2, dblNum1, dblNum2);

            dblNum1 = 2000; dblNum2 = 400;
            objClient.Calculate(3, dblNum1, dblNum2);*/

            Console.WriteLine("Press any key to close ..." );
            Console.ReadKey();
        }
    }
}
