import 'package:flutter/material.dart';

class CustomFormButton extends StatelessWidget {
  final String innerText;
  final void Function()? onPressed;
  const CustomFormButton({
    super.key,
    required this.innerText,
    required this.onPressed,
  });

  @override
  Widget build(BuildContext context) {
    Size size = MediaQuery.of(context).size;
    return Container(
      width: size.width * 0.8,
      decoration: BoxDecoration(
        // color: const Color(0xff233743),
        borderRadius: BorderRadius.circular(26),
      ),
      child: ElevatedButton(
        onPressed: onPressed,
        // style: ElevatedButton.styleFrom(),
        child: Text(
          innerText,
          style: Theme.of(context).textTheme.bodyLarge!.copyWith(
                color: Theme.of(context).colorScheme.onPrimary,
                fontSize: 20,
              ),
        ),
      ),
    );
  }
}
