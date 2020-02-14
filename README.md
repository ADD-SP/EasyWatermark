# EasyWatermark

方便地为图片添加水印的库。

A library to add watermarks to pictures.

# Build

On Windows:
```powershell
dotnet bulid -c Release
```

# How to use

引用"EasyWatermark.dll"后添下列代码即可。

You should reference "EasyWatermark.dll" to your project and add the following code:
```C#
using EasyWatermark;
```

# Example

原图如下（图片来自网络）：

Take this picture as an example：

![](.image/origin.jpg)

## First

给target.jpg的右下角添加一行字，内容为“Hello World”，字体为“思源宋体 CN Heavy”，字体大小50像素，白色，透明度200，处理后的图片保存为“result0.jpg”。

The watermark content is “Hello World”, the font is “思源宋体 CN Heavy”, the size is 50 pixels, the color is white, and the alpha is 200.

```C#
Image watermark = EWatermark.GetWatermark("Hello World", 50, new FontFamily("思源宋体 CN Heavy"), Color.White, 200);

Image result = EWatermark.AddWatermark("target.jpg", watermark);

result.Save("result0.jpg", ImageFormat.Jpeg);
```
![](.image/result0.jpg)

## Second

```C#
Image watermark = EWatermark.GetWatermark("测试用水印", 50, new FontFamily("思源宋体 CN Heavy"), Color.White, 200);

Image result = EWatermark.AddWatermark("target.jpg", watermark);

result.Save("result1.jpg", ImageFormat.Jpeg);
```

![](.image/result1.jpg)

## Third

以watermark.jpg为水印图片为target.jpg打上水印。

Use “watermark.jpg” as a watermark picture to watermark “target.jpg”.


```C#
Image result = EWatermark.AddWatermark("target.jpg", "watermark.jpg");

result.Save("result.jpg", ImageFormat.Jpeg);
```

# Note

+ 这个库的性能较低，建议应用于性能不敏感的场景。

+ The performance of this library is not good, it is recommended to use it as needed.