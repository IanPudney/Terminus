using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Interpreter;

namespace Interpreter
{
    public class VarDecl : Stmt
    {
        Dictionary<string, object> scope;
        string name;
        public VarDecl(Dictionary<string, object> scope, string name)
        {
            this.scope = scope;
            this.name = name;
        }
        public override object Eval()
        {
            scope[name] = null;
            return null;
        }
    }
    public class StackVarDecl : Stmt
    {
        Stack<Dictionary<string, object>> scopeStack;
        string name;
        public StackVarDecl(Stack<Dictionary<string, object>> scopeStack, string name)
        {
            this.scopeStack = scopeStack;
            this.name = name;
        }
        public override object Eval()
        {
            scopeStack.Peek()[name] = null;
            return null;
        }
    }
    public abstract class LValue : Expr
    {
        public abstract void Assign(object o);
    }
    public class Variable : LValue
    {
        Dictionary<string, object> scope;
        string name;
        public Variable(Dictionary<string, object> scope, string name)
        {
            this.scope = scope;
            this.name = name;
        }
        public override void Assign(object o)
        {
            scope[name] = o;
        }
        public override object Eval()
        {
            return scope[name];
        }

    }
    public class StackVariable : LValue
    {
        Stack<Dictionary<string, object>> scopeStack;
        string name;
        public StackVariable(Stack<Dictionary<string, object>> scopeStack, string name)
        {
            this.scopeStack = scopeStack;
            this.name = name;
        }
        public override void Assign(object o)
        {
            scopeStack.Peek()[name] = o;
        }
        public override object Eval()
        {
            return scopeStack.Peek()[name];
        }

    }
}
