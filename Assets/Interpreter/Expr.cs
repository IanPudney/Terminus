using System;
using System.Collections.Generic;
using System.Text;

namespace Interpreter
{
    public abstract class Expr : Stmt
    {

    }
    public class Constant : Expr
    {
        public object val;
        public Constant(object o)
        {
            val = o;
        }
        public override object Eval()
        {
            return val;
        }
    }
    public abstract class BinaryOpExpr : Expr
    {
        public Expr left;
        public Expr right;
        public abstract OperationManager.BinaryOp GetOp();
        public BinaryOpExpr(Expr left, Expr right)
        {
            this.left = left;
            this.right = right;
        }
        public override object Eval()
        {
            object l = left.Eval();
            object r = right.Eval();
            BinaryOperatorDelegate del = OperationManager.binaryDelegateTable[GetOp()][l.GetType()][r.GetType()];
            return del.Invoke(l, r);
        }
    }
    public class AddExpr : BinaryOpExpr
    {
        public AddExpr(Expr left, Expr right) : base(left, right) { }
        public override OperationManager.BinaryOp GetOp()
        {
            return OperationManager.BinaryOp.Add;
        }
    }

    public class SubExpr : BinaryOpExpr
    {
        public SubExpr(Expr left, Expr right) : base(left, right) { }
        public override OperationManager.BinaryOp GetOp()
        {
            return OperationManager.BinaryOp.Sub;
        }
    }

    public class MultExpr : BinaryOpExpr
    {
        public MultExpr(Expr left, Expr right) : base(left, right) { }
        public override OperationManager.BinaryOp GetOp()
        {
            return OperationManager.BinaryOp.Mult;
        }
    }

    public class DivExpr : BinaryOpExpr
    {
        public DivExpr(Expr left, Expr right) : base(left, right) { }
        public override OperationManager.BinaryOp GetOp()
        {
            return OperationManager.BinaryOp.Div;
        }
    }

    public class ModExpr : BinaryOpExpr
    {
        public ModExpr(Expr left, Expr right) : base(left, right) { }
        public override OperationManager.BinaryOp GetOp()
        {
            return OperationManager.BinaryOp.Mod;
        }
    }

    public class PowExpr : BinaryOpExpr
    {
        public PowExpr(Expr left, Expr right) : base(left, right) { }
        public override OperationManager.BinaryOp GetOp()
        {
            return OperationManager.BinaryOp.Pow;
        }
    }

    public class EqExpr : BinaryOpExpr
    {
        public EqExpr(Expr left, Expr right) : base(left, right) { }
        public override OperationManager.BinaryOp GetOp()
        {
            return OperationManager.BinaryOp.Eq;
        }
    }

    public class NotEqExpr : BinaryOpExpr
    {
        public NotEqExpr(Expr left, Expr right) : base(left, right) { }
        public override OperationManager.BinaryOp GetOp()
        {
            return OperationManager.BinaryOp.NotEq;
        }
    }

    public class GtrExpr : BinaryOpExpr
    {
        public GtrExpr(Expr left, Expr right) : base(left, right) { }
        public override OperationManager.BinaryOp GetOp()
        {
            return OperationManager.BinaryOp.Gtr;
        }
    }

    public class LessExpr : BinaryOpExpr
    {
        public LessExpr(Expr left, Expr right) : base(left, right) { }
        public override OperationManager.BinaryOp GetOp()
        {
            return OperationManager.BinaryOp.Less;
        }
    }

    public class GtrEqExpr : BinaryOpExpr
    {
        public GtrEqExpr(Expr left, Expr right) : base(left, right) { }
        public override OperationManager.BinaryOp GetOp()
        {
            return OperationManager.BinaryOp.GtrEq;
        }
    }

    public class LessEqExpr : BinaryOpExpr
    {
        public LessEqExpr(Expr left, Expr right) : base(left, right) { }
        public override OperationManager.BinaryOp GetOp()
        {
            return OperationManager.BinaryOp.LessEq;
        }
    }

    public class AndExpr : BinaryOpExpr
    {
        public AndExpr(Expr left, Expr right) : base(left, right) { }
        public override OperationManager.BinaryOp GetOp()
        {
            return OperationManager.BinaryOp.And;
        }
    }

    public class OrExpr : BinaryOpExpr
    {
        public OrExpr(Expr left, Expr right) : base(left, right) { }
        public override OperationManager.BinaryOp GetOp()
        {
            return OperationManager.BinaryOp.Or;
        }
    }

    public class BAndExpr : BinaryOpExpr
    {
        public BAndExpr(Expr left, Expr right) : base(left, right) { }
        public override OperationManager.BinaryOp GetOp()
        {
            return OperationManager.BinaryOp.BAnd;
        }
    }

    public class BOrExpr : BinaryOpExpr
    {
        public BOrExpr(Expr left, Expr right) : base(left, right) { }
        public override OperationManager.BinaryOp GetOp()
        {
            return OperationManager.BinaryOp.BOr;
        }
    }

    public class IndexExpr : BinaryOpExpr
    {
        public IndexExpr(Expr var, Expr subscript) : base(var, subscript) { }
        public override OperationManager.BinaryOp GetOp()
        {
            return OperationManager.BinaryOp.Index;
        }
    }



    public abstract class UnaryOpExpr : Expr
    {
        public Expr arg;
        public abstract OperationManager.UnaryOp GetOp();
        public abstract string GetOpName();
        public UnaryOpExpr(Expr arg)
        {
            this.arg = arg;
        }
        public override object Eval()
        {
            object o = arg.Eval();
            Dictionary<Type, UnaryOperatorDelegate> opTable;
            if(OperationManager.unaryDelegateTable.TryGetValue(GetOp(), out opTable))
            {
                UnaryOperatorDelegate del;
                if(opTable.TryGetValue(o.GetType(), out del))
                {
                    return del.Invoke(o);
                }
            }
            o.GetType().GetMethod(GetOpName()).Invoke(o, null);
            return false;
            
        }
    }

    public class NotExpr : UnaryOpExpr
    {
        public NotExpr(Expr arg) : base(arg) { }
        public override OperationManager.UnaryOp GetOp()
        {
            return OperationManager.UnaryOp.Not;
        }
        public override string GetOpName()
        {
            return "op_LogicalNot";
        }
    }
    public class NegateExpr : UnaryOpExpr
    {
        public NegateExpr(Expr arg) : base(arg) { }
        public override OperationManager.UnaryOp GetOp()
        {
            return OperationManager.UnaryOp.Negate;
        }
        public override string GetOpName()
        {
            return "op_UnaryNegation";
        }
    }

    public class BNotExpr : UnaryOpExpr
    {
        public BNotExpr(Expr arg) : base(arg) { }
        public override OperationManager.UnaryOp GetOp()
        {
            return OperationManager.UnaryOp.BNot;
        }
        public override string GetOpName()
        {
            return "op_OnesCompliment";
        }
    }

    public class AssignExpr : Expr
    {
        LValue left;
        Expr right;
        public AssignExpr(LValue left, Expr right)
        {
            this.left = left;
            this.right = right;
        }
        public override object Eval()
        {
            left.Assign(right.Eval());
            return left.Eval();
        }
    }
 }
