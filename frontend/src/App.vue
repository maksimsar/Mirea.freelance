<template>
  <div id="app">
    <AppHeader />
    <main>
      <router-view />
    </main>
    <AppFooter />
  </div>
</template>

<script>
import AppHeader from './components/AppHeader.vue';
import AppFooter from './components/AppFooter.vue';

export default {
  name: 'App',
  components: {
    AppHeader,
    AppFooter,
  },
};
</script>

<style>
/* Глобальные стили */
html, body {
  height: 100%;
  margin: 0;
  
}

#app {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
 
}
main {
  flex: 1; /* Занимает всё свободное пространство */
  background-color: #0D0F1A;
  color: #E0E0E0;
}

@font-face {
	font-family: 'BezierSans-Regular';
	src: url('./fonts/beziersans-regular.eot'); /* IE 9 Compatibility Mode */
	src: url('./fonts/beziersans-regular.eot?#iefix') format('embedded-opentype'), /* IE < 9 */
		url('./fonts/beziersans-regular.woff2') format('woff2'), /* Super Modern Browsers */
		url('./fonts/beziersans-regular.woff') format('woff'), /* Firefox >= 3.6, any other modern browser */
		url('./fonts/beziersans-regular.ttf') format('truetype'), /* Safari, Android, iOS */
		url('./fonts/beziersans-regular.svg#beziersans-regular') format('svg'); /* Chrome < 4, Legacy iOS */
}
</style>
