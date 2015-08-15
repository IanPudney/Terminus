using System;
using System.Collections.Generic;
using System.Text;
using Interpreter;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace Interpreter
{
    public delegate object BinaryOperatorDelegate(object left, object right);
    public delegate object UnaryOperatorDelegate(object val);
    static public class OperationManager
    {
        public enum BinaryOp
        {
            Add, Sub, Mult, Div, Mod, Pow, Assign, Eq, NotEq, Gtr, Less, GtrEq, LessEq, And, Or, BAnd, BOr, Index
        }
        public enum UnaryOp
        {
            Negate, Not, BNot
        }
        static public Dictionary<BinaryOp, Dictionary<Type, Dictionary<Type, BinaryOperatorDelegate>>> binaryDelegateTable { get; private set; }
        static public Dictionary<UnaryOp, Dictionary<Type, UnaryOperatorDelegate>> unaryDelegateTable { get; private set; }


        static OperationManager()
        {
            List<object> l;
            binaryDelegateTable = new Dictionary<BinaryOp, Dictionary<Type, Dictionary<Type, BinaryOperatorDelegate>>>();
            unaryDelegateTable = new Dictionary<UnaryOp, Dictionary<Type, UnaryOperatorDelegate>>();
            foreach (BinaryOp op in Enum.GetValues(typeof(BinaryOp)))
            {
                binaryDelegateTable[op] = new Dictionary<Type, Dictionary<Type, BinaryOperatorDelegate>>();
                binaryDelegateTable[op][typeof(int)] = new Dictionary<Type, BinaryOperatorDelegate>();
                binaryDelegateTable[op][typeof(long)] = new Dictionary<Type, BinaryOperatorDelegate>();
                binaryDelegateTable[op][typeof(float)] = new Dictionary<Type, BinaryOperatorDelegate>();
                binaryDelegateTable[op][typeof(double)] = new Dictionary<Type, BinaryOperatorDelegate>();
                binaryDelegateTable[op][typeof(bool)] = new Dictionary<Type, BinaryOperatorDelegate>();
                binaryDelegateTable[op][typeof(string)] = new Dictionary<Type, BinaryOperatorDelegate>();
            }
            foreach (UnaryOp op in Enum.GetValues(typeof(UnaryOp)))
            {
                unaryDelegateTable[op] = new Dictionary<Type, UnaryOperatorDelegate> ();
            }
            binaryDelegateTable[BinaryOp.Add][typeof(int)][typeof(int)] = Add_int_int;
            binaryDelegateTable[BinaryOp.Add][typeof(int)][typeof(long)] = Add_int_long;
            binaryDelegateTable[BinaryOp.Add][typeof(int)][typeof(float)] = Add_int_float;
            binaryDelegateTable[BinaryOp.Add][typeof(int)][typeof(double)] = Add_int_double;
            binaryDelegateTable[BinaryOp.Add][typeof(long)][typeof(int)] = Add_long_int;
            binaryDelegateTable[BinaryOp.Add][typeof(long)][typeof(long)] = Add_long_long;
            binaryDelegateTable[BinaryOp.Add][typeof(long)][typeof(float)] = Add_long_float;
            binaryDelegateTable[BinaryOp.Add][typeof(long)][typeof(double)] = Add_long_double;
            binaryDelegateTable[BinaryOp.Add][typeof(float)][typeof(int)] = Add_float_int;
            binaryDelegateTable[BinaryOp.Add][typeof(float)][typeof(long)] = Add_float_long;
            binaryDelegateTable[BinaryOp.Add][typeof(float)][typeof(float)] = Add_float_float;
            binaryDelegateTable[BinaryOp.Add][typeof(float)][typeof(double)] = Add_float_double;
            binaryDelegateTable[BinaryOp.Add][typeof(double)][typeof(int)] = Add_double_int;
            binaryDelegateTable[BinaryOp.Add][typeof(double)][typeof(long)] = Add_double_long;
            binaryDelegateTable[BinaryOp.Add][typeof(double)][typeof(float)] = Add_double_float;
            binaryDelegateTable[BinaryOp.Add][typeof(double)][typeof(double)] = Add_double_double;
            binaryDelegateTable[BinaryOp.Sub][typeof(int)][typeof(int)] = Sub_int_int;
            binaryDelegateTable[BinaryOp.Sub][typeof(int)][typeof(long)] = Sub_int_long;
            binaryDelegateTable[BinaryOp.Sub][typeof(int)][typeof(float)] = Sub_int_float;
            binaryDelegateTable[BinaryOp.Sub][typeof(int)][typeof(double)] = Sub_int_double;
            binaryDelegateTable[BinaryOp.Sub][typeof(long)][typeof(int)] = Sub_long_int;
            binaryDelegateTable[BinaryOp.Sub][typeof(long)][typeof(long)] = Sub_long_long;
            binaryDelegateTable[BinaryOp.Sub][typeof(long)][typeof(float)] = Sub_long_float;
            binaryDelegateTable[BinaryOp.Sub][typeof(long)][typeof(double)] = Sub_long_double;
            binaryDelegateTable[BinaryOp.Sub][typeof(float)][typeof(int)] = Sub_float_int;
            binaryDelegateTable[BinaryOp.Sub][typeof(float)][typeof(long)] = Sub_float_long;
            binaryDelegateTable[BinaryOp.Sub][typeof(float)][typeof(float)] = Sub_float_float;
            binaryDelegateTable[BinaryOp.Sub][typeof(float)][typeof(double)] = Sub_float_double;
            binaryDelegateTable[BinaryOp.Sub][typeof(double)][typeof(int)] = Sub_double_int;
            binaryDelegateTable[BinaryOp.Sub][typeof(double)][typeof(long)] = Sub_double_long;
            binaryDelegateTable[BinaryOp.Sub][typeof(double)][typeof(float)] = Sub_double_float;
            binaryDelegateTable[BinaryOp.Sub][typeof(double)][typeof(double)] = Sub_double_double;
            binaryDelegateTable[BinaryOp.Mult][typeof(int)][typeof(int)] = Mult_int_int;
            binaryDelegateTable[BinaryOp.Mult][typeof(int)][typeof(long)] = Mult_int_long;
            binaryDelegateTable[BinaryOp.Mult][typeof(int)][typeof(float)] = Mult_int_float;
            binaryDelegateTable[BinaryOp.Mult][typeof(int)][typeof(double)] = Mult_int_double;
            binaryDelegateTable[BinaryOp.Mult][typeof(long)][typeof(int)] = Mult_long_int;
            binaryDelegateTable[BinaryOp.Mult][typeof(long)][typeof(long)] = Mult_long_long;
            binaryDelegateTable[BinaryOp.Mult][typeof(long)][typeof(float)] = Mult_long_float;
            binaryDelegateTable[BinaryOp.Mult][typeof(long)][typeof(double)] = Mult_long_double;
            binaryDelegateTable[BinaryOp.Mult][typeof(float)][typeof(int)] = Mult_float_int;
            binaryDelegateTable[BinaryOp.Mult][typeof(float)][typeof(long)] = Mult_float_long;
            binaryDelegateTable[BinaryOp.Mult][typeof(float)][typeof(float)] = Mult_float_float;
            binaryDelegateTable[BinaryOp.Mult][typeof(float)][typeof(double)] = Mult_float_double;
            binaryDelegateTable[BinaryOp.Mult][typeof(double)][typeof(int)] = Mult_double_int;
            binaryDelegateTable[BinaryOp.Mult][typeof(double)][typeof(long)] = Mult_double_long;
            binaryDelegateTable[BinaryOp.Mult][typeof(double)][typeof(float)] = Mult_double_float;
            binaryDelegateTable[BinaryOp.Mult][typeof(double)][typeof(double)] = Mult_double_double;
            binaryDelegateTable[BinaryOp.Pow][typeof(int)][typeof(int)] = Pow_int_int;
            binaryDelegateTable[BinaryOp.Pow][typeof(int)][typeof(long)] = Pow_int_long;
            binaryDelegateTable[BinaryOp.Pow][typeof(int)][typeof(float)] = Pow_int_float;
            binaryDelegateTable[BinaryOp.Pow][typeof(int)][typeof(double)] = Pow_int_double;
            binaryDelegateTable[BinaryOp.Pow][typeof(long)][typeof(int)] = Pow_long_int;
            binaryDelegateTable[BinaryOp.Pow][typeof(long)][typeof(long)] = Pow_long_long;
            binaryDelegateTable[BinaryOp.Pow][typeof(long)][typeof(float)] = Pow_long_float;
            binaryDelegateTable[BinaryOp.Pow][typeof(long)][typeof(double)] = Pow_long_double;
            binaryDelegateTable[BinaryOp.Pow][typeof(float)][typeof(int)] = Pow_float_int;
            binaryDelegateTable[BinaryOp.Pow][typeof(float)][typeof(long)] = Pow_float_long;
            binaryDelegateTable[BinaryOp.Pow][typeof(float)][typeof(float)] = Pow_float_float;
            binaryDelegateTable[BinaryOp.Pow][typeof(float)][typeof(double)] = Pow_float_double;
            binaryDelegateTable[BinaryOp.Pow][typeof(double)][typeof(int)] = Pow_double_int;
            binaryDelegateTable[BinaryOp.Pow][typeof(double)][typeof(long)] = Pow_double_long;
            binaryDelegateTable[BinaryOp.Pow][typeof(double)][typeof(float)] = Pow_double_float;
            binaryDelegateTable[BinaryOp.Pow][typeof(double)][typeof(double)] = Pow_double_double;
            binaryDelegateTable[BinaryOp.Div][typeof(int)][typeof(int)] = Div_int_int;
            binaryDelegateTable[BinaryOp.Div][typeof(int)][typeof(long)] = Div_int_long;
            binaryDelegateTable[BinaryOp.Div][typeof(int)][typeof(float)] = Div_int_float;
            binaryDelegateTable[BinaryOp.Div][typeof(int)][typeof(double)] = Div_int_double;
            binaryDelegateTable[BinaryOp.Div][typeof(long)][typeof(int)] = Div_long_int;
            binaryDelegateTable[BinaryOp.Div][typeof(long)][typeof(long)] = Div_long_long;
            binaryDelegateTable[BinaryOp.Div][typeof(long)][typeof(float)] = Div_long_float;
            binaryDelegateTable[BinaryOp.Div][typeof(long)][typeof(double)] = Div_long_double;
            binaryDelegateTable[BinaryOp.Div][typeof(float)][typeof(int)] = Div_float_int;
            binaryDelegateTable[BinaryOp.Div][typeof(float)][typeof(long)] = Div_float_long;
            binaryDelegateTable[BinaryOp.Div][typeof(float)][typeof(float)] = Div_float_float;
            binaryDelegateTable[BinaryOp.Div][typeof(float)][typeof(double)] = Div_float_double;
            binaryDelegateTable[BinaryOp.Div][typeof(double)][typeof(int)] = Div_double_int;
            binaryDelegateTable[BinaryOp.Div][typeof(double)][typeof(long)] = Div_double_long;
            binaryDelegateTable[BinaryOp.Div][typeof(double)][typeof(float)] = Div_double_float;
            binaryDelegateTable[BinaryOp.Div][typeof(double)][typeof(double)] = Div_double_double;
            binaryDelegateTable[BinaryOp.Mod][typeof(int)][typeof(int)] = Mod_int_int;
            binaryDelegateTable[BinaryOp.Mod][typeof(int)][typeof(long)] = Mod_int_long;
            binaryDelegateTable[BinaryOp.Mod][typeof(int)][typeof(float)] = Mod_int_float;
            binaryDelegateTable[BinaryOp.Mod][typeof(int)][typeof(double)] = Mod_int_double;
            binaryDelegateTable[BinaryOp.Mod][typeof(long)][typeof(int)] = Mod_long_int;
            binaryDelegateTable[BinaryOp.Mod][typeof(long)][typeof(long)] = Mod_long_long;
            binaryDelegateTable[BinaryOp.Mod][typeof(long)][typeof(float)] = Mod_long_float;
            binaryDelegateTable[BinaryOp.Mod][typeof(long)][typeof(double)] = Mod_long_double;
            binaryDelegateTable[BinaryOp.Mod][typeof(float)][typeof(int)] = Mod_float_int;
            binaryDelegateTable[BinaryOp.Mod][typeof(float)][typeof(long)] = Mod_float_long;
            binaryDelegateTable[BinaryOp.Mod][typeof(float)][typeof(float)] = Mod_float_float;
            binaryDelegateTable[BinaryOp.Mod][typeof(float)][typeof(double)] = Mod_float_double;
            binaryDelegateTable[BinaryOp.Mod][typeof(double)][typeof(int)] = Mod_double_int;
            binaryDelegateTable[BinaryOp.Mod][typeof(double)][typeof(long)] = Mod_double_long;
            binaryDelegateTable[BinaryOp.Mod][typeof(double)][typeof(float)] = Mod_double_float;
            binaryDelegateTable[BinaryOp.Mod][typeof(double)][typeof(double)] = Mod_double_double;
            binaryDelegateTable[BinaryOp.Eq][typeof(int)][typeof(int)] = Eq_int_int;
            binaryDelegateTable[BinaryOp.Eq][typeof(int)][typeof(long)] = Eq_int_long;
            binaryDelegateTable[BinaryOp.Eq][typeof(int)][typeof(float)] = Eq_int_float;
            binaryDelegateTable[BinaryOp.Eq][typeof(int)][typeof(double)] = Eq_int_double;
            binaryDelegateTable[BinaryOp.Eq][typeof(long)][typeof(int)] = Eq_long_int;
            binaryDelegateTable[BinaryOp.Eq][typeof(long)][typeof(long)] = Eq_long_long;
            binaryDelegateTable[BinaryOp.Eq][typeof(long)][typeof(float)] = Eq_long_float;
            binaryDelegateTable[BinaryOp.Eq][typeof(long)][typeof(double)] = Eq_long_double;
            binaryDelegateTable[BinaryOp.Eq][typeof(float)][typeof(int)] = Eq_float_int;
            binaryDelegateTable[BinaryOp.Eq][typeof(float)][typeof(long)] = Eq_float_long;
            binaryDelegateTable[BinaryOp.Eq][typeof(float)][typeof(float)] = Eq_float_float;
            binaryDelegateTable[BinaryOp.Eq][typeof(float)][typeof(double)] = Eq_float_double;
            binaryDelegateTable[BinaryOp.Eq][typeof(double)][typeof(int)] = Eq_double_int;
            binaryDelegateTable[BinaryOp.Eq][typeof(double)][typeof(long)] = Eq_double_long;
            binaryDelegateTable[BinaryOp.Eq][typeof(double)][typeof(float)] = Eq_double_float;
            binaryDelegateTable[BinaryOp.Eq][typeof(double)][typeof(double)] = Eq_double_double;
            binaryDelegateTable[BinaryOp.NotEq][typeof(int)][typeof(int)] = NotEq_int_int;
            binaryDelegateTable[BinaryOp.NotEq][typeof(int)][typeof(long)] = NotEq_int_long;
            binaryDelegateTable[BinaryOp.NotEq][typeof(int)][typeof(float)] = NotEq_int_float;
            binaryDelegateTable[BinaryOp.NotEq][typeof(int)][typeof(double)] = NotEq_int_double;
            binaryDelegateTable[BinaryOp.NotEq][typeof(long)][typeof(int)] = NotEq_long_int;
            binaryDelegateTable[BinaryOp.NotEq][typeof(long)][typeof(long)] = NotEq_long_long;
            binaryDelegateTable[BinaryOp.NotEq][typeof(long)][typeof(float)] = NotEq_long_float;
            binaryDelegateTable[BinaryOp.NotEq][typeof(long)][typeof(double)] = NotEq_long_double;
            binaryDelegateTable[BinaryOp.NotEq][typeof(float)][typeof(int)] = NotEq_float_int;
            binaryDelegateTable[BinaryOp.NotEq][typeof(float)][typeof(long)] = NotEq_float_long;
            binaryDelegateTable[BinaryOp.NotEq][typeof(float)][typeof(float)] = NotEq_float_float;
            binaryDelegateTable[BinaryOp.NotEq][typeof(float)][typeof(double)] = NotEq_float_double;
            binaryDelegateTable[BinaryOp.NotEq][typeof(double)][typeof(int)] = NotEq_double_int;
            binaryDelegateTable[BinaryOp.NotEq][typeof(double)][typeof(long)] = NotEq_double_long;
            binaryDelegateTable[BinaryOp.NotEq][typeof(double)][typeof(float)] = NotEq_double_float;
            binaryDelegateTable[BinaryOp.NotEq][typeof(double)][typeof(double)] = NotEq_double_double;
            binaryDelegateTable[BinaryOp.Gtr][typeof(int)][typeof(int)] = Gtr_int_int;
            binaryDelegateTable[BinaryOp.Gtr][typeof(int)][typeof(long)] = Gtr_int_long;
            binaryDelegateTable[BinaryOp.Gtr][typeof(int)][typeof(float)] = Gtr_int_float;
            binaryDelegateTable[BinaryOp.Gtr][typeof(int)][typeof(double)] = Gtr_int_double;
            binaryDelegateTable[BinaryOp.Gtr][typeof(long)][typeof(int)] = Gtr_long_int;
            binaryDelegateTable[BinaryOp.Gtr][typeof(long)][typeof(long)] = Gtr_long_long;
            binaryDelegateTable[BinaryOp.Gtr][typeof(long)][typeof(float)] = Gtr_long_float;
            binaryDelegateTable[BinaryOp.Gtr][typeof(long)][typeof(double)] = Gtr_long_double;
            binaryDelegateTable[BinaryOp.Gtr][typeof(float)][typeof(int)] = Gtr_float_int;
            binaryDelegateTable[BinaryOp.Gtr][typeof(float)][typeof(long)] = Gtr_float_long;
            binaryDelegateTable[BinaryOp.Gtr][typeof(float)][typeof(float)] = Gtr_float_float;
            binaryDelegateTable[BinaryOp.Gtr][typeof(float)][typeof(double)] = Gtr_float_double;
            binaryDelegateTable[BinaryOp.Gtr][typeof(double)][typeof(int)] = Gtr_double_int;
            binaryDelegateTable[BinaryOp.Gtr][typeof(double)][typeof(long)] = Gtr_double_long;
            binaryDelegateTable[BinaryOp.Gtr][typeof(double)][typeof(float)] = Gtr_double_float;
            binaryDelegateTable[BinaryOp.Gtr][typeof(double)][typeof(double)] = Gtr_double_double;
            binaryDelegateTable[BinaryOp.Less][typeof(int)][typeof(int)] = Less_int_int;
            binaryDelegateTable[BinaryOp.Less][typeof(int)][typeof(long)] = Less_int_long;
            binaryDelegateTable[BinaryOp.Less][typeof(int)][typeof(float)] = Less_int_float;
            binaryDelegateTable[BinaryOp.Less][typeof(int)][typeof(double)] = Less_int_double;
            binaryDelegateTable[BinaryOp.Less][typeof(long)][typeof(int)] = Less_long_int;
            binaryDelegateTable[BinaryOp.Less][typeof(long)][typeof(long)] = Less_long_long;
            binaryDelegateTable[BinaryOp.Less][typeof(long)][typeof(float)] = Less_long_float;
            binaryDelegateTable[BinaryOp.Less][typeof(long)][typeof(double)] = Less_long_double;
            binaryDelegateTable[BinaryOp.Less][typeof(float)][typeof(int)] = Less_float_int;
            binaryDelegateTable[BinaryOp.Less][typeof(float)][typeof(long)] = Less_float_long;
            binaryDelegateTable[BinaryOp.Less][typeof(float)][typeof(float)] = Less_float_float;
            binaryDelegateTable[BinaryOp.Less][typeof(float)][typeof(double)] = Less_float_double;
            binaryDelegateTable[BinaryOp.Less][typeof(double)][typeof(int)] = Less_double_int;
            binaryDelegateTable[BinaryOp.Less][typeof(double)][typeof(long)] = Less_double_long;
            binaryDelegateTable[BinaryOp.Less][typeof(double)][typeof(float)] = Less_double_float;
            binaryDelegateTable[BinaryOp.Less][typeof(double)][typeof(double)] = Less_double_double;
            binaryDelegateTable[BinaryOp.GtrEq][typeof(int)][typeof(int)] = GtrEq_int_int;
            binaryDelegateTable[BinaryOp.GtrEq][typeof(int)][typeof(long)] = GtrEq_int_long;
            binaryDelegateTable[BinaryOp.GtrEq][typeof(int)][typeof(float)] = GtrEq_int_float;
            binaryDelegateTable[BinaryOp.GtrEq][typeof(int)][typeof(double)] = GtrEq_int_double;
            binaryDelegateTable[BinaryOp.GtrEq][typeof(long)][typeof(int)] = GtrEq_long_int;
            binaryDelegateTable[BinaryOp.GtrEq][typeof(long)][typeof(long)] = GtrEq_long_long;
            binaryDelegateTable[BinaryOp.GtrEq][typeof(long)][typeof(float)] = GtrEq_long_float;
            binaryDelegateTable[BinaryOp.GtrEq][typeof(long)][typeof(double)] = GtrEq_long_double;
            binaryDelegateTable[BinaryOp.GtrEq][typeof(float)][typeof(int)] = GtrEq_float_int;
            binaryDelegateTable[BinaryOp.GtrEq][typeof(float)][typeof(long)] = GtrEq_float_long;
            binaryDelegateTable[BinaryOp.GtrEq][typeof(float)][typeof(float)] = GtrEq_float_float;
            binaryDelegateTable[BinaryOp.GtrEq][typeof(float)][typeof(double)] = GtrEq_float_double;
            binaryDelegateTable[BinaryOp.GtrEq][typeof(double)][typeof(int)] = GtrEq_double_int;
            binaryDelegateTable[BinaryOp.GtrEq][typeof(double)][typeof(long)] = GtrEq_double_long;
            binaryDelegateTable[BinaryOp.GtrEq][typeof(double)][typeof(float)] = GtrEq_double_float;
            binaryDelegateTable[BinaryOp.GtrEq][typeof(double)][typeof(double)] = GtrEq_double_double;
            binaryDelegateTable[BinaryOp.LessEq][typeof(int)][typeof(int)] = LessEq_int_int;
            binaryDelegateTable[BinaryOp.LessEq][typeof(int)][typeof(long)] = LessEq_int_long;
            binaryDelegateTable[BinaryOp.LessEq][typeof(int)][typeof(float)] = LessEq_int_float;
            binaryDelegateTable[BinaryOp.LessEq][typeof(int)][typeof(double)] = LessEq_int_double;
            binaryDelegateTable[BinaryOp.LessEq][typeof(long)][typeof(int)] = LessEq_long_int;
            binaryDelegateTable[BinaryOp.LessEq][typeof(long)][typeof(long)] = LessEq_long_long;
            binaryDelegateTable[BinaryOp.LessEq][typeof(long)][typeof(float)] = LessEq_long_float;
            binaryDelegateTable[BinaryOp.LessEq][typeof(long)][typeof(double)] = LessEq_long_double;
            binaryDelegateTable[BinaryOp.LessEq][typeof(float)][typeof(int)] = LessEq_float_int;
            binaryDelegateTable[BinaryOp.LessEq][typeof(float)][typeof(long)] = LessEq_float_long;
            binaryDelegateTable[BinaryOp.LessEq][typeof(float)][typeof(float)] = LessEq_float_float;
            binaryDelegateTable[BinaryOp.LessEq][typeof(float)][typeof(double)] = LessEq_float_double;
            binaryDelegateTable[BinaryOp.LessEq][typeof(double)][typeof(int)] = LessEq_double_int;
            binaryDelegateTable[BinaryOp.LessEq][typeof(double)][typeof(long)] = LessEq_double_long;
            binaryDelegateTable[BinaryOp.LessEq][typeof(double)][typeof(float)] = LessEq_double_float;
            binaryDelegateTable[BinaryOp.LessEq][typeof(double)][typeof(double)] = LessEq_double_double;
            binaryDelegateTable[BinaryOp.BAnd][typeof(int)][typeof(int)] = BAnd_int_int;
            binaryDelegateTable[BinaryOp.BAnd][typeof(int)][typeof(long)] = BAnd_int_long;
            binaryDelegateTable[BinaryOp.BAnd][typeof(long)][typeof(int)] = BAnd_long_int;
            binaryDelegateTable[BinaryOp.BAnd][typeof(long)][typeof(long)] = BAnd_long_long;
            binaryDelegateTable[BinaryOp.BOr][typeof(int)][typeof(int)] = BOr_int_int;
            binaryDelegateTable[BinaryOp.BOr][typeof(int)][typeof(long)] = BOr_int_long;
            binaryDelegateTable[BinaryOp.BOr][typeof(long)][typeof(int)] = BOr_long_int;
            binaryDelegateTable[BinaryOp.BOr][typeof(long)][typeof(long)] = BOr_long_long;
            binaryDelegateTable[BinaryOp.And][typeof(bool)][typeof(bool)] = And_bool_bool;
            binaryDelegateTable[BinaryOp.Or][typeof(bool)][typeof(bool)] = Or_bool_bool;
            binaryDelegateTable[BinaryOp.Eq][typeof(bool)][typeof(bool)] = Eq_bool_bool;
            binaryDelegateTable[BinaryOp.NotEq][typeof(bool)][typeof(bool)] = NotEq_bool_bool;
            binaryDelegateTable[BinaryOp.Add][typeof(string)][typeof(string)] = Add_string_string;
            binaryDelegateTable[BinaryOp.Eq][typeof(string)][typeof(string)] = Eq_string_string;
            binaryDelegateTable[BinaryOp.NotEq][typeof(string)][typeof(string)] = NotEq_string_string;

            unaryDelegateTable[UnaryOp.Not][typeof(bool)] = Not_bool;
            unaryDelegateTable[UnaryOp.BNot][typeof(int)] = BNot_int;
            unaryDelegateTable[UnaryOp.BNot][typeof(long)] = BNot_long;
            unaryDelegateTable[UnaryOp.Negate][typeof(int)] = Negate_int;
            unaryDelegateTable[UnaryOp.Negate][typeof(long)] = Negate_long;
            unaryDelegateTable[UnaryOp.Negate][typeof(float)] = Negate_float;
            unaryDelegateTable[UnaryOp.Negate][typeof(double)] = Negate_double;
        }
        //operators
        //Add, Sub, Mult, Div, Mod, Pow, Assign, Eq, NotEq, Gtr, Less, GtrEq, LessEq, And, Or, BAnd, BOr
        static object Add_int_int(object left, object right) { return (int)left + (int)right; }
        static object Add_int_long(object left, object right) { return (int)left + (long)right; }
        static object Add_int_float(object left, object right) { return (int)left + (float)right; }
        static object Add_int_double(object left, object right) { return (int)left + (double)right; }
        static object Add_long_int(object left, object right) { return (long)left + (int)right; }
        static object Add_long_long(object left, object right) { return (long)left + (long)right; }
        static object Add_long_float(object left, object right) { return (long)left + (float)right; }
        static object Add_long_double(object left, object right) { return (long)left + (double)right; }
        static object Add_float_int(object left, object right) { return (float)left + (int)right; }
        static object Add_float_long(object left, object right) { return (float)left + (long)right; }
        static object Add_float_float(object left, object right) { return (float)left + (float)right; }
        static object Add_float_double(object left, object right) { return (float)left + (double)right; }
        static object Add_double_int(object left, object right) { return (double)left + (int)right; }
        static object Add_double_long(object left, object right) { return (double)left + (long)right; }
        static object Add_double_float(object left, object right) { return (double)left + (float)right; }
        static object Add_double_double(object left, object right) { return (double)left + (double)right; }
        static object Sub_int_int(object left, object right) { return (int)left - (int)right; }
        static object Sub_int_long(object left, object right) { return (int)left - (long)right; }
        static object Sub_int_float(object left, object right) { return (int)left - (float)right; }
        static object Sub_int_double(object left, object right) { return (int)left - (double)right; }
        static object Sub_long_int(object left, object right) { return (long)left - (int)right; }
        static object Sub_long_long(object left, object right) { return (long)left - (long)right; }
        static object Sub_long_float(object left, object right) { return (long)left - (float)right; }
        static object Sub_long_double(object left, object right) { return (long)left - (double)right; }
        static object Sub_float_int(object left, object right) { return (float)left - (int)right; }
        static object Sub_float_long(object left, object right) { return (float)left - (long)right; }
        static object Sub_float_float(object left, object right) { return (float)left - (float)right; }
        static object Sub_float_double(object left, object right) { return (float)left - (double)right; }
        static object Sub_double_int(object left, object right) { return (double)left - (int)right; }
        static object Sub_double_long(object left, object right) { return (double)left - (long)right; }
        static object Sub_double_float(object left, object right) { return (double)left - (float)right; }
        static object Sub_double_double(object left, object right) { return (double)left - (double)right; }
        static object Mult_int_int(object left, object right) { return (int)left * (int)right; }
        static object Mult_int_long(object left, object right) { return (int)left * (long)right; }
        static object Mult_int_float(object left, object right) { return (int)left * (float)right; }
        static object Mult_int_double(object left, object right) { return (int)left * (double)right; }
        static object Mult_long_int(object left, object right) { return (long)left * (int)right; }
        static object Mult_long_long(object left, object right) { return (long)left * (long)right; }
        static object Mult_long_float(object left, object right) { return (long)left * (float)right; }
        static object Mult_long_double(object left, object right) { return (long)left * (double)right; }
        static object Mult_float_int(object left, object right) { return (float)left * (int)right; }
        static object Mult_float_long(object left, object right) { return (float)left * (long)right; }
        static object Mult_float_float(object left, object right) { return (float)left * (float)right; }
        static object Mult_float_double(object left, object right) { return (float)left * (double)right; }
        static object Mult_double_int(object left, object right) { return (double)left * (int)right; }
        static object Mult_double_long(object left, object right) { return (double)left * (long)right; }
        static object Mult_double_float(object left, object right) { return (double)left * (float)right; }
        static object Mult_double_double(object left, object right) { return (double)left * (double)right; }
        static object Pow_int_int(object left, object right) { return Math.Pow((int)left, (int)right); }
        static object Pow_int_long(object left, object right) { return Math.Pow((int)left, (long)right); }
        static object Pow_int_float(object left, object right) { return Math.Pow((int)left, (float)right); }
        static object Pow_int_double(object left, object right) { return Math.Pow((int)left, (double)right); }
        static object Pow_long_int(object left, object right) { return Math.Pow((long)left, (int)right); }
        static object Pow_long_long(object left, object right) { return Math.Pow((long)left, (long)right); }
        static object Pow_long_float(object left, object right) { return Math.Pow((long)left, (float)right); }
        static object Pow_long_double(object left, object right) { return Math.Pow((long)left, (double)right); }
        static object Pow_float_int(object left, object right) { return Math.Pow((float)left, (int)right); }
        static object Pow_float_long(object left, object right) { return Math.Pow((float)left, (long)right); }
        static object Pow_float_float(object left, object right) { return Math.Pow((float)left, (float)right); }
        static object Pow_float_double(object left, object right) { return Math.Pow((float)left, (double)right); }
        static object Pow_double_int(object left, object right) { return Math.Pow((double)left, (int)right); }
        static object Pow_double_long(object left, object right) { return Math.Pow((double)left, (long)right); }
        static object Pow_double_float(object left, object right) { return Math.Pow((double)left, (float)right); }
        static object Pow_double_double(object left, object right) { return Math.Pow((double)left, (double)right); }
        static object Div_int_int(object left, object right) { return (int)left / (int)right; }
        static object Div_int_long(object left, object right) { return (int)left / (long)right; }
        static object Div_int_float(object left, object right) { return (int)left / (float)right; }
        static object Div_int_double(object left, object right) { return (int)left / (double)right; }
        static object Div_long_int(object left, object right) { return (long)left / (int)right; }
        static object Div_long_long(object left, object right) { return (long)left / (long)right; }
        static object Div_long_float(object left, object right) { return (long)left / (float)right; }
        static object Div_long_double(object left, object right) { return (long)left / (double)right; }
        static object Div_float_int(object left, object right) { return (float)left / (int)right; }
        static object Div_float_long(object left, object right) { return (float)left / (long)right; }
        static object Div_float_float(object left, object right) { return (float)left / (float)right; }
        static object Div_float_double(object left, object right) { return (float)left / (double)right; }
        static object Div_double_int(object left, object right) { return (double)left / (int)right; }
        static object Div_double_long(object left, object right) { return (double)left / (long)right; }
        static object Div_double_float(object left, object right) { return (double)left / (float)right; }
        static object Div_double_double(object left, object right) { return (double)left / (double)right; }
        static object Mod_int_int(object left, object right) { return (int)left % (int)right; }
        static object Mod_int_long(object left, object right) { return (int)left % (long)right; }
        static object Mod_int_float(object left, object right) { return (int)left % (float)right; }
        static object Mod_int_double(object left, object right) { return (int)left % (double)right; }
        static object Mod_long_int(object left, object right) { return (long)left % (int)right; }
        static object Mod_long_long(object left, object right) { return (long)left % (long)right; }
        static object Mod_long_float(object left, object right) { return (long)left % (float)right; }
        static object Mod_long_double(object left, object right) { return (long)left % (double)right; }
        static object Mod_float_int(object left, object right) { return (float)left % (int)right; }
        static object Mod_float_long(object left, object right) { return (float)left % (long)right; }
        static object Mod_float_float(object left, object right) { return (float)left % (float)right; }
        static object Mod_float_double(object left, object right) { return (float)left % (double)right; }
        static object Mod_double_int(object left, object right) { return (double)left % (int)right; }
        static object Mod_double_long(object left, object right) { return (double)left % (long)right; }
        static object Mod_double_float(object left, object right) { return (double)left % (float)right; }
        static object Mod_double_double(object left, object right) { return (double)left % (double)right; }
        static object Eq_int_int(object left, object right) { return (int)left == (int)right; }
        static object Eq_int_long(object left, object right) { return (int)left == (long)right; }
        static object Eq_int_float(object left, object right) { return (int)left == (float)right; }
        static object Eq_int_double(object left, object right) { return (int)left == (double)right; }
        static object Eq_long_int(object left, object right) { return (long)left == (int)right; }
        static object Eq_long_long(object left, object right) { return (long)left == (long)right; }
        static object Eq_long_float(object left, object right) { return (long)left == (float)right; }
        static object Eq_long_double(object left, object right) { return (long)left == (double)right; }
        static object Eq_float_int(object left, object right) { return (float)left == (int)right; }
        static object Eq_float_long(object left, object right) { return (float)left == (long)right; }
        static object Eq_float_float(object left, object right) { return (float)left == (float)right; }
        static object Eq_float_double(object left, object right) { return (float)left == (double)right; }
        static object Eq_double_int(object left, object right) { return (double)left == (int)right; }
        static object Eq_double_long(object left, object right) { return (double)left == (long)right; }
        static object Eq_double_float(object left, object right) { return (double)left == (float)right; }
        static object Eq_double_double(object left, object right) { return (double)left == (double)right; }
        static object NotEq_int_int(object left, object right) { return (int)left != (int)right; }
        static object NotEq_int_long(object left, object right) { return (int)left != (long)right; }
        static object NotEq_int_float(object left, object right) { return (int)left != (float)right; }
        static object NotEq_int_double(object left, object right) { return (int)left != (double)right; }
        static object NotEq_long_int(object left, object right) { return (long)left != (int)right; }
        static object NotEq_long_long(object left, object right) { return (long)left != (long)right; }
        static object NotEq_long_float(object left, object right) { return (long)left != (float)right; }
        static object NotEq_long_double(object left, object right) { return (long)left != (double)right; }
        static object NotEq_float_int(object left, object right) { return (float)left != (int)right; }
        static object NotEq_float_long(object left, object right) { return (float)left != (long)right; }
        static object NotEq_float_float(object left, object right) { return (float)left != (float)right; }
        static object NotEq_float_double(object left, object right) { return (float)left != (double)right; }
        static object NotEq_double_int(object left, object right) { return (double)left != (int)right; }
        static object NotEq_double_long(object left, object right) { return (double)left != (long)right; }
        static object NotEq_double_float(object left, object right) { return (double)left != (float)right; }
        static object NotEq_double_double(object left, object right) { return (double)left != (double)right; }
        static object Gtr_int_int(object left, object right) { return (int)left > (int)right; }
        static object Gtr_int_long(object left, object right) { return (int)left > (long)right; }
        static object Gtr_int_float(object left, object right) { return (int)left > (float)right; }
        static object Gtr_int_double(object left, object right) { return (int)left > (double)right; }
        static object Gtr_long_int(object left, object right) { return (long)left > (int)right; }
        static object Gtr_long_long(object left, object right) { return (long)left > (long)right; }
        static object Gtr_long_float(object left, object right) { return (long)left > (float)right; }
        static object Gtr_long_double(object left, object right) { return (long)left > (double)right; }
        static object Gtr_float_int(object left, object right) { return (float)left > (int)right; }
        static object Gtr_float_long(object left, object right) { return (float)left > (long)right; }
        static object Gtr_float_float(object left, object right) { return (float)left > (float)right; }
        static object Gtr_float_double(object left, object right) { return (float)left > (double)right; }
        static object Gtr_double_int(object left, object right) { return (double)left > (int)right; }
        static object Gtr_double_long(object left, object right) { return (double)left > (long)right; }
        static object Gtr_double_float(object left, object right) { return (double)left > (float)right; }
        static object Gtr_double_double(object left, object right) { return (double)left > (double)right; }
        static object Less_int_int(object left, object right) { return (int)left < (int)right; }
        static object Less_int_long(object left, object right) { return (int)left < (long)right; }
        static object Less_int_float(object left, object right) { return (int)left < (float)right; }
        static object Less_int_double(object left, object right) { return (int)left < (double)right; }
        static object Less_long_int(object left, object right) { return (long)left < (int)right; }
        static object Less_long_long(object left, object right) { return (long)left < (long)right; }
        static object Less_long_float(object left, object right) { return (long)left < (float)right; }
        static object Less_long_double(object left, object right) { return (long)left < (double)right; }
        static object Less_float_int(object left, object right) { return (float)left < (int)right; }
        static object Less_float_long(object left, object right) { return (float)left < (long)right; }
        static object Less_float_float(object left, object right) { return (float)left < (float)right; }
        static object Less_float_double(object left, object right) { return (float)left < (double)right; }
        static object Less_double_int(object left, object right) { return (double)left < (int)right; }
        static object Less_double_long(object left, object right) { return (double)left < (long)right; }
        static object Less_double_float(object left, object right) { return (double)left < (float)right; }
        static object Less_double_double(object left, object right) { return (double)left < (double)right; }
        static object GtrEq_int_int(object left, object right) { return (int)left >= (int)right; }
        static object GtrEq_int_long(object left, object right) { return (int)left >= (long)right; }
        static object GtrEq_int_float(object left, object right) { return (int)left >= (float)right; }
        static object GtrEq_int_double(object left, object right) { return (int)left >= (double)right; }
        static object GtrEq_long_int(object left, object right) { return (long)left >= (int)right; }
        static object GtrEq_long_long(object left, object right) { return (long)left >= (long)right; }
        static object GtrEq_long_float(object left, object right) { return (long)left >= (float)right; }
        static object GtrEq_long_double(object left, object right) { return (long)left >= (double)right; }
        static object GtrEq_float_int(object left, object right) { return (float)left >= (int)right; }
        static object GtrEq_float_long(object left, object right) { return (float)left >= (long)right; }
        static object GtrEq_float_float(object left, object right) { return (float)left >= (float)right; }
        static object GtrEq_float_double(object left, object right) { return (float)left >= (double)right; }
        static object GtrEq_double_int(object left, object right) { return (double)left >= (int)right; }
        static object GtrEq_double_long(object left, object right) { return (double)left >= (long)right; }
        static object GtrEq_double_float(object left, object right) { return (double)left >= (float)right; }
        static object GtrEq_double_double(object left, object right) { return (double)left >= (double)right; }
        static object LessEq_int_int(object left, object right) { return (int)left <= (int)right; }
        static object LessEq_int_long(object left, object right) { return (int)left <= (long)right; }
        static object LessEq_int_float(object left, object right) { return (int)left <= (float)right; }
        static object LessEq_int_double(object left, object right) { return (int)left <= (double)right; }
        static object LessEq_long_int(object left, object right) { return (long)left <= (int)right; }
        static object LessEq_long_long(object left, object right) { return (long)left <= (long)right; }
        static object LessEq_long_float(object left, object right) { return (long)left <= (float)right; }
        static object LessEq_long_double(object left, object right) { return (long)left <= (double)right; }
        static object LessEq_float_int(object left, object right) { return (float)left <= (int)right; }
        static object LessEq_float_long(object left, object right) { return (float)left <= (long)right; }
        static object LessEq_float_float(object left, object right) { return (float)left <= (float)right; }
        static object LessEq_float_double(object left, object right) { return (float)left <= (double)right; }
        static object LessEq_double_int(object left, object right) { return (double)left <= (int)right; }
        static object LessEq_double_long(object left, object right) { return (double)left <= (long)right; }
        static object LessEq_double_float(object left, object right) { return (double)left <= (float)right; }
        static object LessEq_double_double(object left, object right) { return (double)left <= (double)right; }
        static object BAnd_int_int(object left, object right) { return (int)left & (int)right; }
        static object BAnd_int_long(object left, object right) { return (int)left & (long)right; }
        static object BAnd_long_int(object left, object right) { return (long)left & (int)right; }
        static object BAnd_long_long(object left, object right) { return (long)left & (long)right; }
        static object BOr_int_int(object left, object right) { return (int)left | (int)right; }
        static object BOr_int_long(object left, object right) { return (int)left | (long)right; }
        static object BOr_long_int(object left, object right) { return (long)left | (int)right; }
        static object BOr_long_long(object left, object right) { return (long)left | (long)right; }
        static object And_bool_bool(object left, object right) { return (bool)left && (bool)right; }
        static object Or_bool_bool(object left, object right) { return (bool)left || (bool)right; }
        static object Eq_bool_bool(object left, object right) { return (bool)left == (bool)right; }
        static object NotEq_bool_bool(object left, object right) { return (bool)left != (bool)right; }
        static object Add_string_string(object left, object right) { return (string)left + (string)right; }
        static object Eq_string_string(object left, object right) { return (string)left == (string)right; }
        static object NotEq_string_string(object left, object right) { return (string)left != (string)right; }

        /*
            unaryDelegateTable[UnaryOp.Not][typeof(bool)] = Not_bool;
            unaryDelegateTable[UnaryOp.BNot][typeof(int)] = BNot_int;
            unaryDelegateTable[UnaryOp.BNot][typeof(long)] = BNot_long;
            unaryDelegateTable[UnaryOp.Negate][typeof(int)] = Negate_int;
            unaryDelegateTable[UnaryOp.Negate][typeof(long)] = Negate_long;
            unaryDelegateTable[UnaryOp.Negate][typeof(float)] = Negate_float;
            unaryDelegateTable[UnaryOp.Negate][typeof(double)] = Negate_double;
        */
        static object Not_bool(object arg) { return !(bool)arg; }
        static object BNot_int(object arg) { return ~(int)arg; }
        static object BNot_long(object arg) { return ~(long)arg; }
        static object Negate_int(object arg) { return -(int)arg; }
        static object Negate_long(object arg) { return -(long)arg; }
        static object Negate_float(object arg) { return -(float)arg; }
        static object Negate_double(object arg) { return -(double)arg; }


    }
}
