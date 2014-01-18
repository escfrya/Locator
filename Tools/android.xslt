<?xml version="1.0"?>
<xsl:stylesheet exclude-result-prefixes="prj" version="1.0" 
  xmlns="http://schemas.microsoft.com/developer/msbuild/2003" 
  xmlns:prj="http://schemas.microsoft.com/developer/msbuild/2003" 
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  
  <xsl:output encoding="UTF-8" indent="yes" method="xml" version="1.0"/>
  
  <!-- Проверяем все тэги и атрибуты, применяем к ним шаблоны -->
  <xsl:template match="@*|node()">
    <xsl:copy>
      <xsl:apply-templates select="@*|node()"/>
    </xsl:copy>
  </xsl:template>
  
  <!-- Удаляем все комментарии -->
  <xsl:template match="comment()"/>
  
  <!-- Подменяем гуиды проекта на нужные для корректного подключения к проекту с приложением -->
  <xsl:template match="prj:ProjectGuid">
    <xsl:copy>
      <xsl:apply-templates select="@*|node()"/>
    </xsl:copy>
    <ProjectTypeGuids>{EFBA0AD7-5A72-4C68-AF49-83D382785DCF};{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}</ProjectTypeGuids>
	<OutputType>Library</OutputType>
    <GenerateSerializationAssemblies>Off</GenerateSerializationAssemblies>
  </xsl:template>
  
  <!-- Удаляем ненужные тэги -->
  <xsl:template match="prj:PreBuildEvent"/>
  <xsl:template match="prj:TargetFrameworkVersion"/>

  <!-- Удаляем ненужные референсы на библиотеки -->
  <xsl:template match="prj:Reference[@Include[contains(.,'Microsoft.CSharp')]]"/>
  <xsl:template match="prj:Reference[@Include[contains(.,'System.Data.DataSetExtensions')]]"/>
  <xsl:template match="prj:Reference[@Include[contains(.,'Wintellect.Sterling.Server')]]"/>
  <xsl:template match="prj:Reference[@Include[contains(.,'Wintellect.Sterling.Server.FileSystem')]]"/>
  <xsl:template match="prj:Reference[@Include[contains(.,'System.ServiceModel')]]"/>

  <!-- Выставляем флаги для мобильных клиентов -->
  <xsl:template match="prj:PropertyGroup[@Condition[contains(.,'Debug')]]/prj:DefineConstants">
    <DefineConstants>TRACE;DEBUG;XAMARIN</DefineConstants>
	<AndroidUseSharedRuntime>True</AndroidUseSharedRuntime>
    <AndroidLinkMode>None</AndroidLinkMode>
  </xsl:template>
  <xsl:template match="prj:PropertyGroup[@Condition[contains(.,'Release')]]/prj:DefineConstants">
    <DefineConstants>TRACE;XAMARIN</DefineConstants>
	<AndroidUseSharedRuntime>False</AndroidUseSharedRuntime>
    <AndroidLinkMode>SdkOnly</AndroidLinkMode>
  </xsl:template>

  <!-- Выставляем версию инструментов для корректной работы в Xamarin Studio -->
  <xsl:template match="prj:Project[@ToolsVersion]/@ToolsVersion">
    <xsl:attribute name="ToolsVersion">
      <xsl:text>4.0</xsl:text>
    </xsl:attribute>
  </xsl:template>
  
  <!-- Для всех референсов в проекте добавляем постфикс .Xamarin -->
  <xsl:template match="prj:ProjectReference/@Include">
    <xsl:attribute name="Include">
      <xsl:call-template name="replace">
        <xsl:with-param name="input" select="."/>
        <xsl:with-param name="from" select="'.csproj'"/>
        <xsl:with-param name="to" select="'.Android.csproj'"/>
      </xsl:call-template>
    </xsl:attribute>
  </xsl:template>

  <!-- В имя проекта подставляем постфикс .Xamarin -->
  <xsl:template match="prj:ProjectReference/prj:Name">
    <Name>
      <xsl:value-of select="."/>
      <xsl:text>.Android</xsl:text>
    </Name>
  </xsl:template>

  <!-- Подменяем пути на платформозависимые библиотеки -->
  <xsl:template match="prj:Reference[@Include[contains(.,'Newtonsoft.Json')]]/prj:HintPath">
    <HintPath>
      <xsl:text>..\Xamarin.Libraries\Newtonsoft.Json.dll</xsl:text>
    </HintPath>
  </xsl:template>
  <xsl:template match="prj:Reference[@Include[contains(.,'RestSharp')]]/prj:HintPath">
    <HintPath>
      <xsl:text>..\Xamarin.Libraries\RestSharp.MonoTouch.dll</xsl:text>
    </HintPath>
  </xsl:template>
  <xsl:template match="prj:Reference[@Include[contains(.,'System.ServiceModel')]]">
    <Reference Include="System.ServiceModel.Xamarin">
      <HintPath>
        <xsl:text>..\Xamarin.Libraries\System.ServiceModel.Xamarin.dll</xsl:text>
      </HintPath>
    </Reference>
  </xsl:template>

  <!-- Прописываем путь к библиотекам стерлинга -->
  <xsl:template match="prj:ProjectReference[@Include[contains(.,'VI.News.ServiceContract.csproj')]]">
    <ProjectReference Include="..\VI.News.ServiceContract\VI.News.ServiceContract.Android.csproj">
      <Project>{F80C00B3-9331-4924-A072-D243AF1BA7D4}</Project>
      <Name>VI.News.ServiceContract.Android</Name>
    </ProjectReference>
    <xsl:if test="//prj:Reference[@Include[contains(.,'Wintellect.Sterling.Server')]]">
      <ProjectReference Include="..\Sterling\Wintellect.Sterling.Xamarin\Wintellect.Sterling.Android.csproj">
        <Project>{E58178FA-1623-4EA9-8B43-A7428295EB4A}</Project>
        <Name>Wintellect.Sterling.Xamarin</Name>
      </ProjectReference>
      <ProjectReference Include="..\Sterling\Wintellect.Sterling.FileSystemDriver.Xamarin\Wintellect.Sterling.FileSystemDriver.Android.csproj">
        <Project>{2C58F3D3-066D-4FA6-91CC-27A7748B9148}</Project>
        <Name>Wintellect.Sterling.FileSystemDriver.Xamarin</Name>
      </ProjectReference>
    </xsl:if>
  </xsl:template>



  <!-- Швблон подмены подстроки в строек -->  
  <xsl:template name="replace">
    <xsl:param name="input"/>
    <xsl:param name="from"/>
    <xsl:param name="to"/>
    <xsl:choose>
      <xsl:when test="contains($input, $from)">
        <xsl:value-of select="substring-before($input, $from)"/>
        <xsl:value-of select="$to"/>
        <xsl:call-template name="replace">
          <xsl:with-param name="input" select="substring-after($input, $from)"/>
          <xsl:with-param name="from" select="$from"/>
          <xsl:with-param name="to" select="$to"/>
        </xsl:call-template>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$input"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <!-- 
  <xsl:template match="prj:ItemGroup/prj:Reference">
    <Reference Include="Mono.Android" />
    <Reference Include="mscorlib" />
    <Reference Include="System" />
    <Reference Include="System.Core" />
    <Reference Include="System.Xml.Linq" />
    <Reference Include="System.Xml" />
  </xsl:template>
  -->  
  <xsl:template match="prj:Import"/>
  <xsl:template match="/*">
	<xsl:copy>
      <xsl:apply-templates select="@*|node()"/>
	  <Import Project="$(MSBuildExtensionsPath)\Xamarin\Android\Xamarin.Android.CSharp.targets" />
    </xsl:copy>
  </xsl:template>

</xsl:stylesheet>