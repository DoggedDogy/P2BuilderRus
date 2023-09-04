using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Lifetime;

using System.Runtime.Serialization.Formatters;

using LibClass;


namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpServerChannel channel = new TcpServerChannel(8086);
            //Регистация канала
            ChannelServices.RegisterChannel(channel, false);
            //Регистрация объекта активизируемыми клиентом
            RemotingConfiguration.ApplicationName = "Character";
            RemotingConfiguration.RegisterActivatedServiceType(typeof(Character));
            Console.WriteLine("Сервер работает");
            Console.WriteLine("Нажмите Enter для выхода");
            Console.ReadLine();//Удерживаем сервер в рабочем состоянии

        }
    }
}
