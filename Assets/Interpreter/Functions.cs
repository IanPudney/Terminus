using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter
{
    public class FunctionDecl : EntryPoint
    {
        public string name;
        public Dictionary<string, object> definitionScope;
        public List<LValue> parameters;
        public Stack<Dictionary<string, object>>  scopeStack;
        public FunctionDecl(Dictionary<string, object> definitionScope, Stack<Dictionary<string, object>> scopeStack, string name, List<LValue> parameters, Stmt body) : base(body)
        {
            this.name = name;
            this.parameters = parameters;
            this.definitionScope = definitionScope;
            this.scopeStack = scopeStack;
        }

        public override object Eval()
        {
            definitionScope[name] = this;
            return null;
        }

        public override object Invoke(params object[] args)
        {
            for (int i = 0; i < Math.Min(args.Length, parameters.Count); ++i)
            {
                parameters[i].Assign(parameters[i]);
            }
            body.Eval();
            object ret;
            scopeStack.Peek().TryGetValue("\0return", out ret);
            return ret;
        }
    }

    public class FunctionCall : Stmt
    {
        public string name;
        public Dictionary<string, object> definitionScope;
        public Stack<Dictionary<string, object>> scopeStack;
        public List<Expr> parameters;
        public FunctionCall (string name, Dictionary<string, object> definitionScope, Stack<Dictionary<string, object>> scopeStack, List<Expr> parameters)
        {
            this.name = name;
            this.definitionScope = definitionScope;
            this.scopeStack = scopeStack;
            this.parameters = parameters;
        }

        public override object Eval()
        {
            object[] parameterResults = new object[parameters.Count];
            for(int i = 0; i < parameters.Count; ++i)
            {
                parameterResults[i] = parameters[i].Eval();
            }

            scopeStack.Push(new Dictionary<string, object>());
            FunctionDecl definition = (FunctionDecl)definitionScope[name];
            object ret = definition.Invoke(scopeStack.Peek(), parameterResults);
            scopeStack.Pop();

            return ret;
        }
    }

    public class ReturnStmt : Stmt
    {
        public Expr toReturn;
        public Stack<Dictionary<string, object>> scopeStack;
        public ReturnStmt (Stack<Dictionary<string, object>> scopeStack, Expr toReturn = null)
        {
            this.scopeStack = scopeStack;
            this.toReturn = toReturn;
        }
        public override object Eval()
        {
            if(this.toReturn != null)
            {
                scopeStack.Peek().Add("\0return", toReturn.Eval());
            }
            return null;
        }
    }
}
