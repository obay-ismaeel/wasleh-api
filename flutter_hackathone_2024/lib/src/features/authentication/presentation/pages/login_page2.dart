import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_hackathone_2024/core/utils/validator_manager.dart';
import 'package:flutter_hackathone_2024/core/utils/values_manager.dart';
import 'package:flutter_hackathone_2024/src/features/authentication/presentation/pages/signup_page2.dart';
import 'package:flutter_hackathone_2024/src/features/core/presentation/cubits/user_cubit/user_cubit.dart';

import '../widgets/widgets.dart';

class LoginPage2 extends StatefulWidget {
  const LoginPage2({super.key});

  @override
  State<LoginPage2> createState() => _LoginPage2State();
}

class _LoginPage2State extends State<LoginPage2> {
  final _formKey = GlobalKey<FormState>();
  final passwordController = TextEditingController();
  final emailController = TextEditingController();

  @override
  Widget build(BuildContext context) {
    Size size = MediaQuery.of(context).size;

    return SafeArea(
      child: SizedBox(
        width: double.infinity,
        child: Scaffold(
          body: Column(
            children: [
              const PageHeader(),
              Expanded(
                child: Container(
                  decoration: const BoxDecoration(
                    // color: Colors.white,
                    borderRadius: BorderRadius.vertical(
                      top: Radius.circular(20),
                    ),
                  ),
                  child: SingleChildScrollView(
                    child: Form(
                      key: _formKey,
                      child: Column(
                        children: [
                          const PageHeading(title: 'Log-in'),
                          CustomInputField(
                            textEditingController: emailController,
                            labelText: 'Email',
                            hintText: 'Your email',
                            validator: (textValue) =>
                                ValidatorManager().validateEmail(textValue!),
                          ),
                          const SizedBox(
                            height: AppSize.s16,
                          ),
                          CustomInputField(
                            textEditingController: passwordController,
                            labelText: 'Password',
                            hintText: 'Your password',
                            obscureText: true,
                            suffixIcon: true,
                            validator: (value) => ValidatorManager.instance
                                .validatePassword(value!),
                          ),
                          const SizedBox(height: AppSize.s16),
                          Container(
                            width: size.width * 0.80,
                            alignment: Alignment.centerRight,
                            child: GestureDetector(
                              // onTap: () => {
                              //   Navigator.push(
                              //     context,
                              //     MaterialPageRoute(
                              //       builder: (_) =>
                              //           BlocProvider<AuthCubit>.value(
                              //         value: context.read<AuthCubit>(),
                              //         child: const ForgetPasswordPage(),
                              //       ),
                              //     ),
                              //   )
                              // },
                              child: Text(
                                'Forget password?',
                                style: Theme.of(context)
                                    .textTheme
                                    .bodySmall!
                                    .copyWith(
                                      color: Theme.of(context)
                                          .colorScheme
                                          .onPrimary,
                                    ),
                              ),
                            ),
                          ),
                          const SizedBox(
                            height: 20,
                          ),
                          CustomFormButton(
                            innerText: 'Login',
                            onPressed: () async =>
                                await context.read<UserCubit>().login(
                                      context,
                                      email: emailController.value.text,
                                      password: passwordController.value.text,
                                    ),
                          ),
                          const SizedBox(
                            height: AppSize.s18,
                          ),
                          SizedBox(
                            width: size.width * 0.8,
                            child: Row(
                              mainAxisAlignment: MainAxisAlignment.center,
                              children: [
                                const Text(
                                  'Don\'t have an account ? ',
                                  // style: TextStyle(
                                  //   fontSize: 13,
                                  //   color: Color(0xff939393),
                                  //   fontWeight: FontWeight.bold,
                                  // ),
                                ),
                                GestureDetector(
                                  onTap: () => {
                                    Navigator.push(
                                      context,
                                      MaterialPageRoute(
                                        builder: (_) => const SignupPage2(),
                                      ),
                                    ),
                                  },
                                  child: Text(
                                    'Sign-up',
                                    style: Theme.of(context)
                                        .textTheme
                                        .bodySmall!
                                        .copyWith(
                                          color: Theme.of(context)
                                              .colorScheme
                                              .onPrimary,
                                        ),
                                  ),
                                ),
                              ],
                            ),
                          ),
                          const SizedBox(
                            height: AppSize.s20,
                          ),
                          TextButton(
                            onPressed: () => {
                              // Navigator.push(
                              //   context,
                              //   MaterialPageRoute(
                              //     builder: (_) => const SignupPage2(),
                              //   ),
                              // ),
                            },
                            child: Text(
                              'Skip',
                              style: Theme.of(context)
                                  .textTheme
                                  .bodySmall!
                                  .copyWith(
                                    color: Theme.of(context).colorScheme.error,
                                  ),
                            ),
                          ),
                        ],
                      ),
                    ),
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
