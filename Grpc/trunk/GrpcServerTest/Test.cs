﻿using MagicOnion;
using MagicOnion.Server;

namespace GrpcServerTest
{
    public interface IHello : IService<IHello>
    {
        UnaryResult<string> Hello(string name);
    }
    public class HelloServices : ServiceBase<IHello>, IHello
    {
        public UnaryResult<string> Hello(string name)
        {
            return new UnaryResult<string>($"hello {name}");
        }
    }
}
