using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace Interpreter
{
    public abstract class EntryPoint : Stmt
    {
        public Stmt body;
        public EntryPoint(Stmt body)
        {
            this.body = body;
        }
        public abstract object Invoke(params object[] parameters);
    }

    public abstract class StaticEventHandler : EntryPoint
    {
        public object objectWithEvent;
        public string eventName;
        public List<LValue> parameters;
        public Stack<Dictionary<string, object>> scopeStack;
        public StaticEventHandler(Stmt body, object objectWithEvent, string eventName, List<LValue> parameters, Stack<Dictionary<string, object>> scopeStack) : base(body)
        {
            this.objectWithEvent = objectWithEvent;
            this.eventName = eventName;
            this.parameters = parameters;
            this.scopeStack = scopeStack;
        }
        bool _enabled = true;
        bool evaluated = false;

        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
                if (_enabled && evaluated)
                {
                    Subscribe();
                }
                else
                {
                    Unsubscribe();
                }
            }
        }
        public override object Eval()
        {
            evaluated = true;
            if (_enabled && evaluated)
            {
                Subscribe();
            }
            else
            {
                Unsubscribe();
            }
            return null;
        }

        public override object Invoke(params object[] args)
        {
            if(parameters != null)
            {
                for (int i = 0; i < Math.Min(args.Length, parameters.Count); ++i)
                {
                    parameters[i].Assign(args[i]);
                }
            }
            body.Eval();
            object ret = null;
            if(scopeStack != null)
            {
                scopeStack.Peek().TryGetValue("\0return", out ret);
            }
            return ret;
        }
        protected virtual void Subscribe()
        {
            Type objectWithEventType = objectWithEvent.GetType();
            EventInfo eventInfo = objectWithEventType.GetEvent(eventName);
            MethodInfo invokeMethod = GetType().GetMethod("InvokeWrapper");
            Delegate handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, this, invokeMethod);

            eventInfo.AddEventHandler(objectWithEvent, handler);
        }

        protected virtual void Unsubscribe()
        {
            Type objectWithEventType = objectWithEvent.GetType();
            EventInfo eventInfo = objectWithEventType.GetEvent(eventName);
            MethodInfo invokeMethod = GetType().GetMethod("InvokeWrapper");
            Delegate handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, invokeMethod);

            eventInfo.RemoveEventHandler(this, handler);
        }
    }

    public class VoidEventHandler : StaticEventHandler
    {
        public VoidEventHandler(Stmt body, object objectWithEvent, string eventName) : base(body, objectWithEvent, eventName, null, null) { }
        public void InvokeWrapper()
        {
            Invoke();
        }
    } 

    public class SenderArgEventHandler : StaticEventHandler
    {
        public SenderArgEventHandler(Stack<Dictionary<string, object>> scopeStack, object objectWithEvent, string eventName, List<LValue> parameters, Stmt body) : base(body, objectWithEvent, eventName, parameters, scopeStack) { }
        public void InvokeWrapper(object sender, object arg)
        {
            Invoke(sender, arg);
        }
    }

    /*public class DynamicEventHandler : EntryPoint
    {
        object objectWithEvent;
        string eventName;
        public DynamicEventHandler(Stmt body, object objectWithEvent, string eventName) : base(body)
        {
            this.objectWithEvent = objectWithEvent;
            this.eventName = eventName;
        }
        bool _enabled = true;
        bool evaluated = false;
        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
                if(_enabled && evaluated)
                {
                    Subscribe();
                } else
                {
                    Unsubscribe();
                }
            }
        }
        public override object Eval()
        {
            evaluated = true;
            if (_enabled && evaluated)
            {
                Subscribe();
            }
            else
            {
                Unsubscribe();
            }
            return null;
        }

        public override object Invoke(params object[] args)
        {
            body.Eval();
            return null;
        }
        public void InvokeVoid()
        {
            Invoke(null, null);
        }


        void Subscribe()
        {
            Type objectWithEventType = objectWithEvent.GetType();
            EventInfo eventInfo = objectWithEventType.GetEvent(eventName);
            MethodInfo addMethod = eventInfo.GetAddMethod();
            ParameterInfo[] delegateParams = eventInfo.EventHandlerType.GetMethod("Invoke").GetParameters();
            Type delegateReturns = eventInfo.EventHandlerType.GetMethod("Invoke").ReflectedType;
            List<Type> ps = new List<Type>();
            //ps.Add(typeof(ActionData));
            foreach (ParameterInfo info in delegateParams)
            {
                ps.Add(info.ParameterType);
            }

            DynamicMethod method = new DynamicMethod("Adapter", delegateReturns, ps.ToArray(), GetType(), true);



            MethodInfo InvokeVoidMethod = this.GetType().GetMethod("InvokeVoid");
            Delegate d = Delegate.CreateDelegate(eventInfo.EventHandlerType, this, InvokeVoidMethod);
            addMethod.Invoke(objectWithEvent, new object[1]{ d });
        }

        void Unsubscribe()
        {
            //todo
        }
    }*/
}
