import 'dart:developer';
import 'dart:io';

import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_hackathone_2024/core/helper/sizer_media_query.dart';
import 'package:flutter_hackathone_2024/core/utils/color_manager.dart';
import 'package:flutter_hackathone_2024/core/utils/style_manager.dart';
import 'package:flutter_hackathone_2024/core/utils/validator_manager.dart';
import 'package:flutter_hackathone_2024/core/utils/values_manager.dart';
import 'package:flutter_hackathone_2024/src/features/authentication/presentation/pages/login_page2.dart';
import 'package:flutter_hackathone_2024/src/features/core/presentation/cubits/user_cubit/user_cubit.dart';
import 'package:image_picker/image_picker.dart';

import '../widgets/widgets.dart';

class SignupPage2 extends StatefulWidget {
  const SignupPage2({super.key});

  @override
  State<SignupPage2> createState() => _SignupPage2State();
}

class _SignupPage2State extends State<SignupPage2> {
  final _formKey = GlobalKey<FormState>();
  final nameController = TextEditingController();
  final emailController = TextEditingController();
  final contactNumberController = TextEditingController();
  final passwordController = TextEditingController();

  XFile? _imagePick;

  final ImagePicker _picker = ImagePicker();

  Future<void> _pickerFromGallery(
      BuildContext context, Function setState) async {
    final imageGallery = await _picker.pickImage(
      source: ImageSource.gallery,
    );
    if (imageGallery != null) {
      setState(() {
        _imagePick = XFile(imageGallery.path);
        Navigator.pop(context);
      });
      log(imageGallery.path);
    }
  }

  Future<void> _pickerFromCamera(
      BuildContext context, Function setState) async {
    final imageCamera = await _picker.pickImage(
      source: ImageSource.camera,
    );
    if (imageCamera != null) {
      setState(() {
        _imagePick = XFile(imageCamera.path);
      });
      Navigator.pop(context);
      log(imageCamera.path);
    }
  }

  void _deletePicker(BuildContext context, Function setState) {
    setState(() {
      _imagePick = null;
    });
    Navigator.pop(context);
  }

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Scaffold(
        // backgroundColor: const Color(0xffEEF1F3),
        body: SingleChildScrollView(
          child: Form(
            key: _formKey,
            child: Column(
              children: [
                const PageHeader(),
                Container(
                  decoration: const BoxDecoration(
                    // color: Colors.white,
                    borderRadius: BorderRadius.vertical(
                      top: Radius.circular(20),
                    ),
                  ),
                  child: Column(
                    children: [
                      const PageHeading(title: 'Sign-up'),
                      StatefulBuilder(builder: (context, setStatePicker) {
                        return SizedBox(
                          width: getWidth(context) * 0.3,
                          height: getWidth(context) * 0.3,
                          child: Stack(
                            alignment: Alignment.center,
                            clipBehavior: Clip.hardEdge,
                            children: [
                              AnimatedContainer(
                                clipBehavior: Clip.hardEdge,
                                duration: const Duration(milliseconds: 300),
                                width: getWidth(context) * 0.3,
                                height: getWidth(context) * 0.3,
                                decoration: BoxDecoration(
                                    color:
                                        Theme.of(context).colorScheme.onPrimary,
                                    shape: BoxShape.circle,
                                    border: _imagePick != null
                                        ? Border.all(
                                            color: ColorManager.secondary,
                                            width: AppSize.s4)
                                        : null),
                                child: _imagePick == null
                                    ? Icon(
                                        Icons.person,
                                        size: getWidth(context) * 0.2,
                                      )
                                    : ClipOval(
                                        child: Image.file(
                                          File(_imagePick!.path),
                                          width: getWidth(context) * 0.3,
                                          height: getWidth(context) * 0.3,
                                          fit: BoxFit.cover,
                                        ),
                                      ),
                              ),
                              Align(
                                alignment: Alignment.bottomRight,
                                child: Positioned(
                                  right: getWidth(context) / 3.25,
                                  bottom: 0,
                                  child: IconButton(
                                    onPressed: () {
                                      showModalBottomSheet(
                                        context: context,
                                        shape: const RoundedRectangleBorder(
                                          borderRadius: BorderRadius.vertical(
                                            top: Radius.circular(AppSize.s24),
                                          ),
                                        ),
                                        builder: (_) => Padding(
                                          padding: const EdgeInsets.only(
                                            top: AppPadding.p12,
                                          ),
                                          child: Column(
                                            mainAxisSize: MainAxisSize.min,
                                            children: [
                                              Container(
                                                width: 76,
                                                height: 5,
                                                decoration: BoxDecoration(
                                                  color: Theme.of(context)
                                                      .colorScheme
                                                      .onPrimary,
                                                  borderRadius:
                                                      BorderRadius.circular(
                                                    100.0,
                                                  ),
                                                ),
                                              ),
                                              const SizedBox(
                                                height: AppSize.s10,
                                              ),
                                              SizedBox(
                                                height: MediaQuery.of(context)
                                                        .size
                                                        .width /
                                                    2.5,
                                                child: Column(
                                                  children: [
                                                    _buildImagePicker(
                                                      title:
                                                          'Pick From Gallery',
                                                      icon: Icons.image,
                                                      onTap: () =>
                                                          _pickerFromGallery(
                                                        context,
                                                        setStatePicker,
                                                      ),
                                                    ),

                                                    ///
                                                    divider(
                                                      indent:
                                                          getWidth(context) /
                                                              AppSize.s6,
                                                    ),
                                                    _buildImagePicker(
                                                        title:
                                                            'Pick From Camera',
                                                        icon: Icons.camera_alt,
                                                        onTap: () =>
                                                            _pickerFromCamera(
                                                                context,
                                                                setStatePicker)),

                                                    ///
                                                    divider(
                                                      indent:
                                                          getWidth(context) /
                                                              AppSize.s6,
                                                    ),
                                                    _buildImagePicker(
                                                      title: 'Delete Photo',
                                                      icon: Icons.delete,
                                                      onTap: () =>
                                                          _deletePicker(
                                                        context,
                                                        setStatePicker,
                                                      ),
                                                    ),
                                                  ],
                                                ),
                                              ),
                                            ],
                                          ),
                                        ),
                                      );
                                    },
                                    icon: CircleAvatar(
                                      backgroundColor: ColorManager.white,
                                      child: Icon(
                                        Icons.add_a_photo,
                                        size: getWidth(context) * 0.05,
                                      ),
                                    ),
                                  ),
                                ),
                              ),
                            ],
                          ),
                        );
                      }),
                    ],
                  ),
                ),
                const SizedBox(
                  height: AppSize.s16,
                ),
                CustomInputField(
                  textEditingController: nameController,
                  labelText: 'Name',
                  hintText: 'Your name',
                  isDense: true,
                  validator: (value) => ValidatorManager().validateName(value!),
                ),
                const SizedBox(
                  height: AppSize.s16,
                ),
                CustomInputField(
                  textEditingController: emailController,
                  labelText: 'Email',
                  hintText: 'Your email',
                  isDense: true,
                  validator: (value) =>
                      ValidatorManager().validateEmail(value!),
                ),
                const SizedBox(
                  height: AppSize.s16,
                ),
                CustomInputField(
                  textEditingController: contactNumberController,
                  labelText: 'Contact no.',
                  hintText: 'Your contact number',
                  isDense: true,
                  validator: (value) =>
                      ValidatorManager().validatePhone(value!),
                ),
                const SizedBox(
                  height: AppSize.s16,
                ),
                CustomInputField(
                  textEditingController: passwordController,
                  labelText: 'Password',
                  hintText: 'Your password',
                  isDense: true,
                  obscureText: true,
                  validator: (value) =>
                      ValidatorManager().validatePassword(value!),
                  suffixIcon: true,
                ),
                const SizedBox(
                  height: AppSize.s24,
                ),
                CustomFormButton(
                  innerText: 'Signup',
                  onPressed: () async =>
                      await context.read<UserCubit>().register(
                            context,
                            email: emailController.value.text,
                            password: passwordController.value.text,
                            firstName: nameController.value.text.split(' ')[0],
                            lastName: nameController.value.text.split(' ')[1],
                            image: _imagePick?.path ?? '',
                          ),
                ),
                const SizedBox(
                  height: AppSize.s18,
                ),
                SizedBox(
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.center,
                    crossAxisAlignment: CrossAxisAlignment.center,
                    children: [
                      const Text(
                        'Already have an account ? ',
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
                              builder: (context) => const LoginPage2(),
                            ),
                          ),
                        },
                        child: Text(
                          'Log-in',
                          style: Theme.of(context)
                              .textTheme
                              .bodySmall!
                              .copyWith(
                                color: Theme.of(context).colorScheme.onPrimary,
                              ),
                          // style: TextStyle(
                          //   fontSize: 15,
                          //   color: Color(0xff748288),
                          //   fontWeight: FontWeight.bold,
                          // ),
                        ),
                      ),
                    ],
                  ),
                ),
                const SizedBox(
                  height: AppSize.s30,
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }

  Widget _buildImagePicker(
      {required String title,
      required IconData icon,
      required void Function()? onTap}) {
    return Expanded(
      child: ListTile(
        onTap: onTap,
        leading: Icon(icon),
        title: Text(title),
        dense: true,
        splashColor: ColorManager.secondary,
        style: ListTileStyle.list,
      ),
    );
  }
}
