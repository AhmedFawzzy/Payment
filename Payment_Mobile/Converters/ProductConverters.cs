using System.Globalization;

namespace Payment_Mobile.Converters;

public class BestValueBorderConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool isBestValue && isBestValue)
            return Color.FromArgb("#137fec");
        return Color.FromArgb("#e2e8f0");
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class BestValueStrokeConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool isBestValue && isBestValue)
            return 2.0;
        return 1.0;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class ButtonTextConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool hasFreeTrial && hasFreeTrial)
            return "Start Free Trial";
        return "Subscribe Now";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class IconColorConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string colorName)
        {
            return colorName switch
            {
                "indigo" => Color.FromArgb("#eef2ff"),
                "amber" => Color.FromArgb("#fffbeb"),
                "emerald" => Color.FromArgb("#ecfdf5"),
                "slate" => Color.FromArgb("#f1f5f9"),
                "primary" => Color.FromArgb("#dbeafe"),
                _ => Color.FromArgb("#f1f5f9")
            };
        }
        return Color.FromArgb("#f1f5f9");
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class IconConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string iconName)
        {
            return iconName switch
            {
                "block" => "🚫",
                "savings" => "💰",
                "rocket_launch" => "🚀",
                "calendar_month" => "📅",
                "verified" => "✓",
                _ => "●"
            };
        }
        return "●";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

