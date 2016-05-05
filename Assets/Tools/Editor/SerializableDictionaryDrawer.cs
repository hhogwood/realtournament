using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Text.RegularExpressions;
using System.Text;


[CustomPropertyDrawer(typeof(DrawableDictionary), true)]
public class DictionaryPropertyDrawer : PropertyDrawer
{

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (property.isExpanded)
        {
            var keysProp = property.FindPropertyRelative("_keys");
            return (keysProp.arraySize + 4) * EditorGUIUtility.singleLineHeight;
        }
        else
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        bool expanded = property.isExpanded;
        var r = GetNextRect(ref position);
        property.isExpanded = EditorGUI.Foldout(r, property.isExpanded, label);

        if (expanded)
        {
            EditorGUI.indentLevel++;

            var keysProp = property.FindPropertyRelative("_keys");
            var valuesProp = property.FindPropertyRelative("_values");

            int cnt = keysProp.arraySize;
            if (valuesProp.arraySize != cnt) valuesProp.arraySize = cnt;

            for (int i = 0; i < cnt; i++)
            {
                r = GetNextRect(ref position);
                r = EditorGUI.IndentedRect(r);
                var w = r.width / 2f;
                var r0 = new Rect(r.xMin, r.yMin, w, r.height);
                var r1 = new Rect(r0.xMax, r.yMin, w, r.height);

                var keyProp = keysProp.GetArrayElementAtIndex(i);
                var valueProp = valuesProp.GetArrayElementAtIndex(i);
                EditorGUI.PropertyField(r0, keyProp, GUIContent.none, false);
                EditorGUI.PropertyField(r1, valueProp, GUIContent.none, false);
            }

            r = GetNextRect(ref position);
            var pRect = new Rect(r.xMax - 60f, r.yMin, 30f, EditorGUIUtility.singleLineHeight);
            var mRect = new Rect(r.xMax - 30f, r.yMin, 30f, EditorGUIUtility.singleLineHeight);

            if (GUI.Button(pRect, "+"))
            {
                keysProp.arraySize++;
                SetPropertyValue(keysProp.GetArrayElementAtIndex(keysProp.arraySize - 1), null);
                valuesProp.arraySize = keysProp.arraySize;
            }
            if (GUI.Button(mRect, "-"))
            {
                keysProp.arraySize = Mathf.Max(keysProp.arraySize - 1, 0);
                valuesProp.arraySize = keysProp.arraySize;
            }
        }
    }


    private Rect GetNextRect(ref Rect position)
    {
        var r = new Rect(position.xMin, position.yMin, position.width, EditorGUIUtility.singleLineHeight);
        var h = EditorGUIUtility.singleLineHeight + 1f;
        position = new Rect(position.xMin, position.yMin + h, position.width, position.height = h);
        return r;
    }

    public static void SetPropertyValue(SerializedProperty prop, object value)
    {
        if (prop == null) throw new System.ArgumentNullException("prop");

        switch (prop.propertyType)
        {
            case SerializedPropertyType.Integer:
                prop.intValue = ToInt(value);
                break;
            case SerializedPropertyType.Boolean:
                prop.boolValue = ToBool(value);
                break;
            case SerializedPropertyType.Float:
                prop.floatValue = ToSingle(value);
                break;
            case SerializedPropertyType.String:
                prop.stringValue = ToString(value);
                break;
            case SerializedPropertyType.Color:
                prop.colorValue = (Color)value;
                break;
            case SerializedPropertyType.ObjectReference:
                prop.objectReferenceValue = value as Object;
                break;
            case SerializedPropertyType.LayerMask:
                prop.intValue = (value is LayerMask) ? ((LayerMask)value).value : ToInt(value);
                break;
            case SerializedPropertyType.Enum:
                //prop.enumValueIndex = ConvertUtil.ToInt(value);
                //prop.SetEnumValue(value);
                throw new System.InvalidOperationException("Can not handle enum types.");
                break;
            case SerializedPropertyType.Vector2:
                prop.vector2Value = (Vector2)value;
                break;
            case SerializedPropertyType.Vector3:
                prop.vector3Value = (Vector3)value;
                break;
            case SerializedPropertyType.Vector4:
                prop.vector4Value = (Vector4)value;
                break;
            case SerializedPropertyType.Rect:
                prop.rectValue = (Rect)value;
                break;
            case SerializedPropertyType.ArraySize:
                prop.arraySize = ToInt(value);
                break;
            case SerializedPropertyType.Character:
                prop.intValue = ToInt(value);
                break;
            case SerializedPropertyType.AnimationCurve:
                prop.animationCurveValue = value as AnimationCurve;
                break;
            case SerializedPropertyType.Bounds:
                prop.boundsValue = (Bounds)value;
                break;
            case SerializedPropertyType.Gradient:
                throw new System.InvalidOperationException("Can not handle Gradient types.");
        }
    }

    #region ConvertToInt

    public static int ToInt(sbyte value)
    {
        return System.Convert.ToInt32(value);
    }

    public static int ToInt(byte value)
    {
        return System.Convert.ToInt32(value);
    }

    public static int ToInt(short value)
    {
        return System.Convert.ToInt32(value);
    }

    public static int ToInt(ushort value)
    {
        return System.Convert.ToInt32(value);
    }

    public static int ToInt(int value)
    {
        return value;
    }

    public static int ToInt(uint value)
    {
        if (value > int.MaxValue)
        {
            return int.MinValue + System.Convert.ToInt32(value & 0x7fffffff);
        }
        else
        {
            return System.Convert.ToInt32(value & 0xffffffff);
        }
    }

    public static int ToInt(long value)
    {
        if (value > int.MaxValue)
        {
            return int.MinValue + System.Convert.ToInt32(value & 0x7fffffff);
        }
        else
        {
            return System.Convert.ToInt32(value & 0xffffffff);
        }
    }

    public static int ToInt(ulong value)
    {
        if (value > int.MaxValue)
        {
            return int.MinValue + System.Convert.ToInt32(value & 0x7fffffff);
        }
        else
        {
            return System.Convert.ToInt32(value & 0xffffffff);
        }
    }

    public static int ToInt(float value)
    {
        return System.Convert.ToInt32(value);
        //if (value > int.MaxValue)
        //{
        //    return int.MinValue + System.Convert.ToInt32(value & 0x7fffffff);
        //}
        //else
        //{
        //    return System.Convert.ToInt32(value & 0xffffffff);
        //}
    }

    public static int ToInt(double value)
    {
        return System.Convert.ToInt32(value);
        //if (value > int.MaxValue)
        //{
        //    return int.MinValue + System.Convert.ToInt32(value & 0x7fffffff);
        //}
        //else
        //{
        //    return System.Convert.ToInt32(value & 0xffffffff);
        //}
    }

    public static int ToInt(decimal value)
    {
        return System.Convert.ToInt32(value);
        //if (value > int.MaxValue)
        //{
        //    return int.MinValue + System.Convert.ToInt32(value & 0x7fffffff);
        //}
        //else
        //{
        //    return System.Convert.ToInt32(value & 0xffffffff);
        //}
    }

    public static int ToInt(bool value)
    {
        return value ? 1 : 0;
    }

    public static int ToInt(char value)
    {
        return System.Convert.ToInt32(value);
    }

    public static int ToInt(object value)
    {
        if (value == null)
        {
            return 0;
        }
        else if (value is System.IConvertible)
        {
            try
            {
                return System.Convert.ToInt32(value);
            }
            catch
            {
                return 0;
            }
        }
        else
        {
            return ToInt(value.ToString());
        }
    }

    public static int ToInt(string value, System.Globalization.NumberStyles style)
    {
        return ToInt(ToDouble(value, style));
    }
    public static int ToInt(string value)
    {
        return ToInt(ToDouble(value, System.Globalization.NumberStyles.Any));
    }
    #endregion

    #region "ToDouble"
    public static double ToDouble(sbyte value)
    {
        return System.Convert.ToDouble(value);
    }

    public static double ToDouble(byte value)
    {
        return System.Convert.ToDouble(value);
    }

    public static double ToDouble(short value)
    {
        return System.Convert.ToDouble(value);
    }

    public static double ToDouble(ushort value)
    {
        return System.Convert.ToDouble(value);
    }

    public static double ToDouble(int value)
    {
        return System.Convert.ToDouble(value);
    }

    public static double ToDouble(uint value)
    {
        return System.Convert.ToDouble(value);
    }

    public static double ToDouble(long value)
    {
        return System.Convert.ToDouble(value);
    }

    public static double ToDouble(ulong value)
    {
        return System.Convert.ToDouble(value);
    }

    public static double ToDouble(float value)
    {
        return System.Convert.ToDouble(value);
    }

    public static double ToDouble(double value)
    {
        return value;
    }

    public static double ToDouble(decimal value)
    {
        return System.Convert.ToDouble(value);
    }

    public static double ToDouble(bool value)
    {
        return value ? 1 : 0;
    }

    public static double ToDouble(char value)
    {
        return ToDouble(System.Convert.ToInt32(value));
    }

    public static double ToDouble(object value)
    {
        if (value == null)
        {
            return 0;
        }
        else if (value is System.IConvertible)
        {
            try
            {
                return System.Convert.ToDouble(value);
            }
            catch
            {
                return 0;
            }
        }
        else
        {
            return ToDouble(value.ToString(), System.Globalization.NumberStyles.Any, null);
        }
    }

    /// <summary>
    /// System.Converts any string to a number with no errors.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="style"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
    /// <remarks>
    /// TODO: I would also like to possibly include support for other number system bases. At least binary and octal.
    /// </remarks>
    public static double ToDouble(string value, System.Globalization.NumberStyles style, System.IFormatProvider provider)
    {
        if (string.IsNullOrEmpty(value)) return 0d;

        style = style & System.Globalization.NumberStyles.Any;
        double dbl = 0;
        if (double.TryParse(value, style, provider, out dbl))
        {
            return dbl;
        }
        else
        {
            //test hex
            int i;
            bool isNeg = false;
            for (i = 0; i < value.Length; i++)
            {
                if (value[i] == ' ' || value[i] == '+') continue;
                if (value[i] == '-')
                {
                    isNeg = !isNeg;
                    continue;
                }
                break;
            }

            if (i < value.Length - 1 &&
                    (
                    (value[i] == '#') ||
                    (value[i] == '0' && (value[i + 1] == 'x' || value[i + 1] == 'X')) ||
                    (value[i] == '&' && (value[i + 1] == 'h' || value[i + 1] == 'H'))
                    ))
            {
                //is hex
                style = (style & System.Globalization.NumberStyles.HexNumber) | System.Globalization.NumberStyles.AllowHexSpecifier;

                if (value[i] == '#') i++;
                else i += 2;
                int j = value.IndexOf('.', i);

                if (j >= 0)
                {
                    long lng = 0;
                    long.TryParse(value.Substring(i, j - i), style, provider, out lng);

                    if (isNeg)
                        lng = -lng;

                    long flng = 0;
                    string sfract = value.Substring(j + 1).Trim();
                    long.TryParse(sfract, style, provider, out flng);
                    return System.Convert.ToDouble(lng) + System.Convert.ToDouble(flng) / System.Math.Pow(16d, sfract.Length);
                }
                else
                {
                    string num = value.Substring(i);
                    long l;
                    if (long.TryParse(num, style, provider, out l))
                        return System.Convert.ToDouble(l);
                    else
                        return 0d;
                }
            }
            else
            {
                return 0d;
            }
        }


        ////################
        ////OLD garbage heavy version

        //if (value == null) return 0d;
        //value = value.Trim();
        //if (string.IsNullOrEmpty(value)) return 0d;

        //#if UNITY_WEBPLAYER
        //			Match m = Regex.Match(value, RX_ISHEX, RegexOptions.IgnoreCase);
        //#else
        //            Match m = Regex.Match(value, RX_ISHEX, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        //#endif

        //if (m.Success)
        //{
        //    long lng = 0;
        //    style = (style & System.Globalization.NumberStyles.HexNumber) | System.Globalization.NumberStyles.AllowHexSpecifier;
        //    long.TryParse(m.Groups["num"].Value, style, provider, out lng);

        //    if (m.Groups["sign"].Value == "-")
        //        lng = -lng;

        //    if (m.Groups["fractional"].Success)
        //    {
        //        long flng = 0;
        //        string sfract = m.Groups["fractional"].Value.Substring(1);
        //        long.TryParse(sfract, style, provider, out flng);
        //        return System.Convert.ToDouble(lng) + System.Convert.ToDouble(flng) / System.Math.Pow(16d, sfract.Length);
        //    }
        //    else
        //    {
        //        return System.Convert.ToDouble(lng);
        //    }

        //}
        //else
        //{
        //    style = style & System.Globalization.NumberStyles.Any;
        //    double dbl = 0;
        //    double.TryParse(value, style, provider, out dbl);
        //    return dbl;

        //}
    }

    public static double ToDouble(string value, System.Globalization.NumberStyles style)
    {
        return ToDouble(value, style, null);
    }

    public static double ToDouble(string value)
    {
        return ToDouble(value, System.Globalization.NumberStyles.Any, null);
    }
    #endregion

    #region "ToDecimal"
    public static decimal ToDecimal(sbyte value)
    {
        return System.Convert.ToDecimal(value);
    }

    public static decimal ToDecimal(byte value)
    {
        return System.Convert.ToDecimal(value);
    }

    public static decimal ToDecimal(short value)
    {
        return System.Convert.ToDecimal(value);
    }

    public static decimal ToDecimal(ushort value)
    {
        return System.Convert.ToDecimal(value);
    }

    public static decimal ToDecimal(int value)
    {
        return System.Convert.ToDecimal(value);
    }

    public static decimal ToDecimal(uint value)
    {
        return System.Convert.ToDecimal(value);
    }

    public static decimal ToDecimal(long value)
    {
        return System.Convert.ToDecimal(value);
    }

    public static decimal ToDecimal(ulong value)
    {
        return System.Convert.ToDecimal(value);
    }

    public static decimal ToDecimal(float value)
    {
        return System.Convert.ToDecimal(value);
    }

    public static decimal ToDecimal(double value)
    {
        return System.Convert.ToDecimal(value);
    }

    public static decimal ToDecimal(decimal value)
    {
        return value;
    }

    public static decimal ToDecimal(bool value)
    {
        return value ? 1 : 0;
    }

    public static decimal ToDecimal(char value)
    {
        return ToDecimal(System.Convert.ToInt32(value));
    }

    public static decimal ToDecimal(object value)
    {
        if (value == null)
        {
            return 0;
        }
        else if (value is System.IConvertible)
        {
            try
            {
                return System.Convert.ToDecimal(value);
            }
            catch
            {
                return 0;
            }
        }
        else
        {
            return ToDecimal(value.ToString());
        }
    }

    public static decimal ToDecimal(string value, System.Globalization.NumberStyles style)
    {
        return System.Convert.ToDecimal(ToDouble(value, style));
    }
    public static decimal ToDecimal(string value)
    {
        return System.Convert.ToDecimal(ToDouble(value, System.Globalization.NumberStyles.Any));
    }
    #endregion

    #region "ToBool"
    public static bool ToBool(sbyte value)
    {
        return value != 0;
    }

    public static bool ToBool(byte value)
    {
        return value != 0;
    }

    public static bool ToBool(short value)
    {
        return value != 0;
    }

    public static bool ToBool(ushort value)
    {
        return value != 0;
    }

    public static bool ToBool(int value)
    {
        return value != 0;
    }

    public static bool ToBool(uint value)
    {
        return value != 0;
    }

    public static bool ToBool(long value)
    {
        return value != 0;
    }

    public static bool ToBool(ulong value)
    {
        return value != 0;
    }

    public static bool ToBool(float value)
    {
        return value != 0;
    }

    public static bool ToBool(double value)
    {
        return value != 0;
    }

    public static bool ToBool(decimal value)
    {
        return value != 0;
    }

    public static bool ToBool(bool value)
    {
        return value;
    }

    public static bool ToBool(char value)
    {
        return System.Convert.ToInt32(value) != 0;
    }

    public static bool ToBool(object value)
    {
        if (value == null)
        {
            return false;
        }
        else if (value is System.IConvertible)
        {
            try
            {
                return System.Convert.ToBoolean(value);
            }
            catch
            {
                return false;
            }
        }
        else
        {
            return ToBool(value.ToString());
        }
    }

    /// <summary>
    /// Converts a string to boolean. Is FALSE greedy.
    /// A string is considered TRUE if it DOES meet one of the following criteria:
    /// 
    /// doesn't read blank: ""
    /// doesn't read false (not case-sensitive)
    /// doesn't read 0
    /// doesn't read off (not case-sensitive)
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    /// <remarks></remarks>
    public static bool ToBool(string str)
    {
        //str = (str + "").Trim().ToLower();
        //return !System.Convert.ToBoolean(string.IsNullOrEmpty(str) || str == "false" || str == "0" || str == "off");

        return !(string.IsNullOrEmpty(str) || str.Equals("false", System.StringComparison.OrdinalIgnoreCase) || str.Equals("0", System.StringComparison.OrdinalIgnoreCase) || str.Equals("off", System.StringComparison.OrdinalIgnoreCase));
    }

    #region "ToString"
    public static string ToString(sbyte value)
    {
        return System.Convert.ToString(value);
    }

    public static string ToString(byte value)
    {
        return System.Convert.ToString(value);
    }

    public static string ToString(short value)
    {
        return System.Convert.ToString(value);
    }

    public static string ToString(ushort value)
    {
        return System.Convert.ToString(value);
    }

    public static string ToString(int value)
    {
        return System.Convert.ToString(value);
    }

    public static string ToString(uint value)
    {
        return System.Convert.ToString(value);
    }

    public static string ToString(long value)
    {
        return System.Convert.ToString(value);
    }

    public static string ToString(ulong value)
    {
        return System.Convert.ToString(value);
    }

    public static string ToString(float value)
    {
        return System.Convert.ToString(value);
    }

    public static string ToString(double value)
    {
        return System.Convert.ToString(value);
    }

    public static string ToString(decimal value)
    {
        return System.Convert.ToString(value);
    }

    public static string ToString(bool value, string sFormat)
    {
        switch (sFormat)
        {
            case "num":
                return (value) ? "1" : "0";
            case "normal":
            case "":
            case null:
                return System.Convert.ToString(value);
            default:
                return System.Convert.ToString(value);
        }
    }

    public static string ToString(bool value)
    {
        return System.Convert.ToString(value);
    }

    public static string ToString(char value)
    {
        return System.Convert.ToString(value);
    }

    public static string ToString(object value)
    {
        return System.Convert.ToString(value);
    }

    public static string ToString(string str)
    {
        return str;
    }
    #endregion
    #endregion
    #region "ToSingle"
    public static float ToSingle(sbyte value)
    {
        return System.Convert.ToSingle(value);
    }

    public static float ToSingle(byte value)
    {
        return System.Convert.ToSingle(value);
    }

    public static float ToSingle(short value)
    {
        return System.Convert.ToSingle(value);
    }

    public static float ToSingle(ushort value)
    {
        return System.Convert.ToSingle(value);
    }

    public static float ToSingle(int value)
    {
        return System.Convert.ToSingle(value);
    }

    public static float ToSingle(uint value)
    {
        return System.Convert.ToSingle(value);
    }

    public static float ToSingle(long value)
    {
        return System.Convert.ToSingle(value);
    }

    public static float ToSingle(ulong value)
    {
        return System.Convert.ToSingle(value);
    }

    public static float ToSingle(float value)
    {
        return System.Convert.ToSingle(value);
    }

    public static float ToSingle(double value)
    {
        return (float)value;
    }

    public static float ToSingle(decimal value)
    {
        return System.Convert.ToSingle(value);
    }

    public static float ToSingle(bool value)
    {
        return value ? 1 : 0;
    }

    public static float ToSingle(char value)
    {
        return ToSingle(System.Convert.ToInt32(value));
    }

    public static float ToSingle(object value)
    {
        if (value == null)
        {
            return 0;
        }
        else if (value is System.IConvertible)
        {
            try
            {
                return System.Convert.ToSingle(value);
            }
            catch
            {
                return 0;
            }
        }
        else
        {
            return ToSingle(value.ToString());
        }
    }

    public static float ToSingle(string value, System.Globalization.NumberStyles style)
    {
        return System.Convert.ToSingle(ToDouble(value, style));
    }
    public static float ToSingle(string value)
    {
        return System.Convert.ToSingle(ToDouble(value, System.Globalization.NumberStyles.Any));
    }
    #endregion
}