using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter
{
    public abstract class Stmt : Node
    {
    }
    
    public class StmtBlock : Stmt
    {
        List<Stmt> stmts;
        public StmtBlock(List<Stmt> stmts)
        {
            this.stmts = stmts;
        }
        public override object Eval()
        {
            foreach (Stmt stmt in stmts)
            {
                stmt.Eval();
            }
            return null;
        }
    }
    public class WhileLoop : Stmt
    {
        Expr condition;
        Stmt loop;
        public WhileLoop(Expr condition, Stmt loop)
        {
            this.condition = condition;
            this.loop = loop;
        }
        public override object Eval()
        {
            while((bool) condition.Eval())
            {
                loop.Eval();
            }
            return null;
        }
    }

    public class ForLoop : Stmt
    {
        Expr init;
        Expr condition;
        Expr step;
        Stmt loop;
        public ForLoop(Expr init, Expr condition, Expr step, Stmt loop)
        {
            this.init = init;
            this.condition = condition;
            this.step = step;
            this.loop = loop;
        }
        public override object Eval()
        {
            for(init.Eval(); (bool) condition.Eval(); step.Eval())
            {
                loop.Eval();
            }
            return null;
        }
    }
}
