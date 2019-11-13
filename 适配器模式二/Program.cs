using System;
/*
 * 介绍：
 * 意图：将一个类的接口转换成客户希望的另外一个接口。适配器模式使得原本由于接口不兼容而不能一起工作的那些类可以一起工作
 * 主要解决：主要解决在软件系统中，常常要将一些“现存的对象”放到新的环境中，而新环境要求的接口是现对象不能满足的。
 * 何时使用：1.系统需要使用现有的类，而此类的接口不符合系统的需要。2.想要建立一个可以重复使用的类，
 * 用于与一些彼此之间没有太大关联的一些类，包括一些可能在将来引进的类一起工作，这些源类不一定有一致的接口。3.通过接口转换，
 * 将一个类插入另一个类系中。（比如老虎和飞禽，现在多了一个飞虎，在不增加实体的需求下，增加一个适配器，在里面包括一个虎对象，实现飞的接口）
 * 如何解决：继承或依赖
 * 关键代码：适配器继承或依赖已有的对象，实现想要的目标接口
 * 应用实例：美国电器110V，中国220V，就要有一个适配器将110V转化为220V
 * 优点：1.可以让任何两个没有关联的类一起运行；2.提高了类的复用。3.增加了类的透明度。4.灵活性好
 * 缺点：1.过多地使用适配器，会让系统非常凌乱，不易整体进行把握。比如，明明看到调用的A接口，其实内部被适配成了B接口的实现。
 * 一个系统如果太多出现这种情况，无异于一场灾难。因此如果不是很有必要，可以不使用适配器，而是直接对系统进行重构
 * 使用场景：有动机地修改一个正常运行的系统的接口，这时应该考虑使用适配器模式。
 * 注意事项：适配器不是在详细设计时添加的，而是解决正在服役的项目的问题
 */
namespace 适配器模式二
{
    class Program
    {
        static void Main(string[] args)
        {
            AudioPlayer audioPlayer = new AudioPlayer();
            audioPlayer.Play("mp3", "she told me.mp3");

            audioPlayer.Play("mp4", "alone.mp4");
            audioPlayer.Play("vlc", "ooooooo.vlc");
            Console.ReadKey();
        }
    }
    //步骤1：为媒体播放器和更高级的媒体播放器创建接口
    public interface IMediaPlayer
    {
        void Play(string audioType, string fileName);
    }
    //步骤2 创建实现了MediaPlayer接口的实体类
    public class AudioPlayer : IMediaPlayer
    {
        //步骤6 把适配器放在AudioPlayer
        MediaAdapter mediaAdapter;
        public void Play(string audioType, string fileName)
        {
            if (audioType.Equals("mp3"))
            {
                Console.WriteLine("播放MP3文件，名称: " + fileName);
            }
            //步骤6 mediaAdapter提供了播放其他文件格式的支持
            else if (audioType.Equals("vlc") || audioType.Equals("mp4"))
            {
                mediaAdapter = new MediaAdapter(audioType);
                mediaAdapter.Play(audioType, fileName);
            }
            else
            {
                Console.WriteLine("无效的媒体。" + audioType + "格式不支持");
            }
        }
    }
    //步骤3：更高级的媒体播放器创建接口
    public interface IAdvancedMediaPlayer
    {
        void PalyVlc(string fileName);
        void PlayMp4(string fileName);
    }
    //步骤4：创建实现了AdvancedMediaPlayer接口的实体类
    public class VlcPalyer : IAdvancedMediaPlayer
    {
        void IAdvancedMediaPlayer.PalyVlc(string fileName)
        {
            Console.WriteLine("播放vlc文件。名称：" + fileName);

        }
        void IAdvancedMediaPlayer.PlayMp4(string fileName)
        {
        }
    }
    public class Mp4Player : IAdvancedMediaPlayer
    {
        void IAdvancedMediaPlayer.PalyVlc(string fileName)
        {
        }

        void IAdvancedMediaPlayer.PlayMp4(string fileName)
        {
            Console.WriteLine("播放mp4文件。名称：" + fileName);
        }
    }
    //步骤5：创建实现了MediaPlayer接口的适配器类
    public class MediaAdapter : IMediaPlayer
    {
        IAdvancedMediaPlayer advancedMediaPlayer;

        public MediaAdapter(string audioType)
        {
            if (audioType.Equals("vlc"))
            {
                advancedMediaPlayer = new VlcPalyer();
            }
            else if (audioType.Equals("mp4"))
            {
                advancedMediaPlayer = new Mp4Player();
            }
        }
        public void Play(string audioType, string fileName)
        {
            if (audioType.Equals("vlc"))
            {
                advancedMediaPlayer.PalyVlc(fileName);
            }
            else if (audioType.Equals("mp4"))
            {
                advancedMediaPlayer.PlayMp4(fileName);
            }
        }
    }
}
