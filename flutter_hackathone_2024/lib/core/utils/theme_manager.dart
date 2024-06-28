import 'package:flutter_hackathone_2024/core/helper/convert_to_material_color.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

import 'color_manager.dart';

class ThemeManager {
  static ThemeData myTheme = ThemeData(
    fontFamily: GoogleFonts.poppins().fontFamily,
    primarySwatch: Color(ColorManager.primary.value).toMaterialColor(),
  ).copyWith(
    primaryColor: ColorManager.primary,
    primaryIconTheme: const IconThemeData(
      color: ColorManager.primary,
    ),
    iconButtonTheme: IconButtonThemeData(
      style: ButtonStyle(
        iconColor: WidgetStateProperty.all(ColorManager.primary),
      ),
    ),
    appBarTheme: const AppBarTheme(
      elevation: 0,
      iconTheme: IconThemeData(color: ColorManager.primary),
      actionsIconTheme: IconThemeData(color: ColorManager.primary),
      backgroundColor: ColorManager.appBarColor,
      centerTitle: true,
    ),
    drawerTheme: const DrawerThemeData(
      backgroundColor: ColorManager.appBarColor,
    ),
    scaffoldBackgroundColor: ColorManager.scaffoldBackgroundLight,
  );

  static ThemeData get light {
    return ThemeData(
      indicatorColor: colorSchemeLight.onPrimary,
      appBarTheme: AppBarTheme(
        centerTitle: true,
        // color: Colors.transparent,
        color: primaryColorLight,
        // shape: const RoundedRectangleBorder(
        //   borderRadius: BorderRadius.vertical(
        //     bottom: Radius.circular(30),
        //   ),
        // ),
        titleTextStyle: GoogleFonts.copse(
          fontSize: 35,
          fontWeight: FontWeight.w900,
          color: primaryFgColorLight,
        ),
        iconTheme: const IconThemeData(
          color: primaryFgColorLight,
          size: 35,
        ),
      ),
      colorScheme: colorSchemeLight,
      snackBarTheme: const SnackBarThemeData(
        behavior: SnackBarBehavior.floating,
      ),
      textTheme: GoogleFonts.aliceTextTheme(
        Typography.blackRedmond,
      ),
      primaryTextTheme: TextTheme(
        displaySmall: GoogleFonts.alice(
          fontSize: 11,
          fontWeight: FontWeight.w500,
          color: Colors.black,
        ),
        bodySmall: GoogleFonts.alice(
          fontSize: 13,
          fontWeight: FontWeight.w600,
          color: Colors.black,
        ),
        bodyMedium: GoogleFonts.alice(
          fontSize: 16,
          fontWeight: FontWeight.w900,
          color: primaryFgColorLight,
        ),
        bodyLarge: GoogleFonts.aBeeZee(
          fontSize: 19,
          fontWeight: FontWeight.w900,
          color: primaryFgColorLight,
        ),
        titleMedium: GoogleFonts.alice(
          fontSize: 25,
          color: Colors.black,
          fontWeight: FontWeight.bold,
        ),
        titleLarge: GoogleFonts.alice(
          fontSize: 38,
          color: Colors.black,
          fontWeight: FontWeight.bold,
        ),
      ),
      tabBarTheme: TabBarTheme(
        indicatorColor: primaryFgColorLight,
        labelStyle: GoogleFonts.alice(
          fontSize: 25,
          fontWeight: FontWeight.w800,
          color: primaryFgColorLight,
        ),
        unselectedLabelStyle: GoogleFonts.alice(
          fontSize: 25,
          fontWeight: FontWeight.w600,
          color: secondaryColorLight.withAlpha(128),
        ),
      ),
      drawerTheme: const DrawerThemeData(
        backgroundColor: primaryColorLight,
        shape: RoundedRectangleBorder(
          borderRadius: BorderRadius.only(
            topRight: Radius.circular(30),
          ),
        ),
        endShape: BeveledRectangleBorder(),
      ),
      dividerTheme: const DividerThemeData(
        thickness: 1,
        color: secondaryColorLight,
      ),
      dialogTheme: const DialogTheme(
        shape: RoundedRectangleBorder(
          borderRadius: BorderRadius.all(
            Radius.circular(30),
          ),
        ),
      ),
      navigationBarTheme: const NavigationBarThemeData(
        backgroundColor: primaryColorLight,
        elevation: 0,
      ),
      bottomNavigationBarTheme: const BottomNavigationBarThemeData(
        type: BottomNavigationBarType.fixed,
        backgroundColor: primaryColorLight,
        elevation: 0,
      ),
      textButtonTheme: const TextButtonThemeData(
        style: ButtonStyle(
          foregroundColor: WidgetStatePropertyAll(primaryColorLight),
        ),
      ),
      buttonTheme: const ButtonThemeData(
        shape: BeveledRectangleBorder(),
        colorScheme: colorSchemeLight,
        textTheme: ButtonTextTheme.primary,
      ),
      cardTheme: const CardTheme(
        color: primaryColorLight,
        clipBehavior: Clip.antiAlias,
        shadowColor: primaryFgColorLight,
        elevation: 5,
        shape: RoundedRectangleBorder(
          borderRadius: BorderRadius.all(
            Radius.circular(30),
          ),
        ),
      ),
    );
  }

  static ThemeData get dark {
    return ThemeData(
      appBarTheme: const AppBarTheme(
        centerTitle: true,
        shape: BeveledRectangleBorder(),
      ),
      colorScheme: colorSchemeDark,
      snackBarTheme: const SnackBarThemeData(
        behavior: SnackBarBehavior.floating,
      ),
      textTheme: GoogleFonts.aliceTextTheme(),
    );
  }

  static const outerSpace = Color(0xFF475256);

  static const textColorLight = Color(0xFF030f00);
  static const backgroundColorLight = Color(0xFFf7fff5);
  static const primaryColorLight = Color(0xFFf7fff5);
  static const primaryFgColorLight = Color(0xFF446440);
  static const secondaryColorLight = Color(0xFF9c9fce);
  static const secondaryFgColorLight = Color(0xFF030f00);
  static const accentColorLight = Color(0xFF6E67D5);
  static const accentFgColorLight = Color(0xFFf7fff5);

  static const colorSchemeLight = ColorScheme(
    brightness: Brightness.light,
    primary: primaryColorLight,
    onPrimary: primaryFgColorLight,
    secondary: secondaryColorLight,
    onSecondary: secondaryFgColorLight,
    tertiary: accentColorLight,
    onTertiary: accentFgColorLight,
    surface: backgroundColorLight,
    onSurface: textColorLight,
    error: Brightness.light == Brightness.light
        ? Color(0xffB3261E)
        : Color(0xffF2B8B5),
    onError: Brightness.light == Brightness.light
        ? Color(0xffFFFFFF)
        : Color(0xff601410),
  );

  static const textColorDark = Color(0xFFf3fff0);
  static const backgroundColorDark = Color(0xFF020a00);
  static const primaryColorDark = Color(0xFFa0bf9b);
  static const primaryFgColorDark = Color(0xFF020a00);
  static const secondaryColorDark = Color(0xFF313463);
  static const secondaryFgColorDark = Color(0xFFf3fff0);
  static const accentColorDark = Color(0xFF312a98);
  static const accentFgColorDark = Color(0xFFf3fff0);

  static const colorSchemeDark = ColorScheme(
    brightness: Brightness.dark,
    primary: primaryColorDark,
    onPrimary: primaryFgColorDark,
    secondary: secondaryColorDark,
    onSecondary: secondaryFgColorDark,
    tertiary: accentColorDark,
    onTertiary: accentFgColorDark,
    surface: backgroundColorDark,
    onSurface: textColorDark,
    error: Brightness.dark == Brightness.light
        ? Color(0xffB3261E)
        : Color(0xffF2B8B5),
    onError: Brightness.dark == Brightness.light
        ? Color(0xffFFFFFF)
        : Color(0xff601410),
  );
}
