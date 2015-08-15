using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Interpreter
{
    public class MemberCall : Expr
    {
        public Expr var;
        public string field;
        public List<Expr> pars;
        public MemberCall(Expr var, string field, List<Expr> pars)
        {
            this.var = var;
            this.field = field;
            this.pars = pars;
        }
        public override object Eval()
        {
            object[] parResults = new object[pars.Count];
            for(int i=0; i<pars.Count; ++i)
            {
                parResults[i] = pars[i].Eval();
            }
            object varResult = var.Eval();
            System.Reflection.MethodInfo methodToCall = varResult.GetType().GetMethod(field);
            return methodToCall.Invoke(varResult, parResults);
        }
    }
    public class FieldAccess : LValue
    {
        public Expr var;
        public string field;
        public FieldAccess(Expr var, string field)
        {
            this.var = var;
            this.field = field;
        }
        public override object Eval()
        {
            object varResult = var.Eval();
            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;

            PropertyInfo prop = varResult.GetType().GetProperty(field);
            if(prop != null)
            {
                return prop.GetValue(varResult, null);
            }
            FieldInfo fiel = varResult.GetType().GetField(field);
            return fiel.GetValue(varResult);
            //todo: put this return in an if stmt too, and throw a better exception
        }

        public override void Assign(object o)
        {
            object varResult = var.Eval();
            PropertyInfo prop = varResult.GetType().GetProperty(field);
            if (prop != null)
            {
                prop.SetValue(varResult, o, null);
                return;
            }
            FieldInfo fiel = varResult.GetType().GetField(field);
            fiel.SetValue(varResult, o);
        }

    }
    public class IndexAccess : LValue
    {
        public Expr left;
        public Expr subscript;
        public IndexAccess(Expr left, Expr subscript)
        {
            this.left = left;
            this.subscript = subscript;
        }
        public override object Eval()
        {
            object leftResult = left.Eval();
            object subscriptResult = subscript.Eval();
            object[] pars = new object[] { subscriptResult };
            return leftResult.GetType().GetMethod("get_Item").Invoke(leftResult, pars);
        }
        public override void Assign(object o)
        {
            object leftResult = left.Eval();
            object subscriptResult = subscript.Eval();
            object[] pars = new object[] { subscriptResult , o};
            leftResult.GetType().GetMethod("set_Item").Invoke(leftResult, pars);
        }
    }

    public class New : Expr
    {
        public Type type;
        public New (Type type)
        {
            this.type = type;
            
        }
        public New (string classname)
        {
            this.type = Type.GetType(classname);
        }
        public override object Eval()
        {
            return Activator.CreateInstance(type);
        }
    }
}
